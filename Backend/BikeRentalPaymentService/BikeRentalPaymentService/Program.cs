using BikeRentalPaymentService;
using BikeRentalPaymentService.Data;
using BikeRentalPaymentService.DTOs;
using BikeRentalPaymentService.Message;
using BikeRentalPaymentService.Messaging;
using BikeRentalPaymentService.Models;
using BikeRentalPaymentService.UseCases;
using MassTransit;
using Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();

builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.SetKebabCaseEndpointNameFormatter();

    busConfiguration.AddConsumer<PaymentRequestConsumer>();

    busConfiguration.UsingRabbitMq((ctx, cfg) =>
    {
        string host = Environment.GetEnvironmentVariable("MQ_HOST")
            ?? builder.Configuration["MessageBroker:Host"]!;

        string userName = Environment.GetEnvironmentVariable("MQ_USER")
            ?? builder.Configuration["MessageBroker:UserName"]!;

        string password = Environment.GetEnvironmentVariable("MQ_PASSWORD")
            ?? builder.Configuration["MessageBroker:Password"]!;

        Uri uri = new(host);
        cfg.Host(uri, h =>
        {
            h.Username(userName);
            h.Password(password);
        });

        cfg.ConfigureEndpoints(ctx);



    });
});

builder.Services.AddSingleton<Settings>();


//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(
//        Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
//        ?? builder.Configuration.GetConnectionString("DefaultConnection")
//    )
//);

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Join(path, "paymentservice.db");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<MessagingClient>();
builder.Services.AddScoped<DoPaymentUseCase>();
builder.Services.AddScoped<PaymentConfirmationUseCase>();


//builder.Services.AddHostedService<PaymentRequestSubscriber>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db?.Database.Migrate();

}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHealthChecks("/api/health");

app.MapPost("/api/v1/do-payment", DoPayment)
.WithName("Do Payment")
.WithOpenApi();

app.MapPost("/api/v1/payment-confirmation/{id}", PaymentConfirmation)
.WithName("Payment Confirmation")
.WithOpenApi();

app.MapGet("/api/v1/payments/{userId}", GetPayments)
.WithName("Get Payments By User")
.WithOpenApi();

app.Run();

static async Task<IResult> DoPayment([FromBody] PaymentRequest request, 
                                      DoPaymentUseCase useCase)
{
    Result result = await useCase.ExecuteAsync(request);
    if(result.HasError())
        return TypedResults.BadRequest(result.Error);
    return TypedResults.NoContent();

}

static async Task PaymentConfirmation([FromRoute] Guid id, 
                                      HttpContext context, 
                                      PaymentRepository repository,
                                      IPublishEndpoint publishEndpoint)
{

    Payment? payment = await repository.GetByIdAsync(id);

    if (payment is null)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.CompleteAsync();
        return;

    }

    payment.Approve();
    var transaction = await repository.BeginTransaction();
    await repository.UpdateAsync(payment);

    PaymentConfirmation paymentConfirmation = new(payment);

    try
    {
        await publishEndpoint.Publish(paymentConfirmation);

        context.Response.StatusCode = StatusCodes.Status204NoContent;
        await context.Response.CompleteAsync();

        await transaction.CommitAsync();
        await transaction.DisposeAsync();


    }
    catch(Exception ex) 
    {
        Console.WriteLine(ex.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.CompleteAsync();
        await transaction.RollbackAsync();
        await transaction.DisposeAsync();
    }

}


static async Task<IResult> GetPayments([FromRoute] string userId, PaymentRepository repository)
{
    IEnumerable<PaymentResponse> response = (await repository
        .GetPaymentsByUserId(userId))
        .Select(x => new PaymentResponse(x));
    return TypedResults.Ok(response);
}

