using Amazon.Runtime;
using Amazon.S3;
using BikeRentalCore.Data;
using BikeRentalCore.Extensions;
using BikeRentalCore.Message;
using BikeRentalCore.Security;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.SetKebabCaseEndpointNameFormatter();

    busConfiguration.AddConsumer<PaymentConfirmationConsumer>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InjectDependencies(builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("InternalPolicy", policy =>
        policy.Requirements.Add(new InternalUserRequirement()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db?.Database.Migrate();

    var dataSeeding = scope.ServiceProvider.GetRequiredService<DataSeeding>();
    dataSeeding.Execute();

}



app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
