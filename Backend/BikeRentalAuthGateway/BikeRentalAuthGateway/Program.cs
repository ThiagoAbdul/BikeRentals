using BikeRentalAuthGateway.Data;
using BikeRentalAuthGateway.DTOs.In;
using BikeRentalAuthGateway.Extensions;
using BikeRentalAuthGateway.Middlewares;
using BikeRentalAuthGateway.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddMessaging(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "all",
        policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

builder.Services.AddHttpClient();

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetService<AppDbContext>();
    context?.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("all");

app.UseHealthChecks("/auth/Health");
app.UseMiddleware<GlobalExceptionHandler>();
app.UseMiddleware<RequestForwardingMiddleware>();

app.MapPost("/auth/sign-in", SignIn)
.WithName("SignIn")
.WithOpenApi();

app.MapPost("/auth/sign-up", SignUp)
.WithName("SignUp")
.WithOpenApi();

app.MapGet("/identity/user/{id}", GetUser)
.WithName("Get user")
.WithOpenApi();

app.Run();

static async Task<IResult> SignIn([FromBody] SignInRequest request, SignInUseCase useCase)
{
    var result = await useCase.ExecuteAsync(request);

    if (result.HasError())
        return result.Error!.ToHttpResult();
    return TypedResults.Ok(result.Value);
}

static async Task<IResult> SignUp([FromBody] SignUpRequest request, SignUpUseCase useCase)
{
    var result = await useCase.ExecuteAsync(request);

    if (result.HasError())
        return result.Error!.ToHttpResult();
    return TypedResults.Ok(result.Value);
}

static async Task<IResult> GetUser([FromRoute] Guid id, [FromServices] GetUserUseCase useCase, HttpContext context, IConfiguration configuration)
{
    string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? configuration["Api:Key"]!;

    if (context.Request.ApiKey() != apiKey)
        return TypedResults.Unauthorized();

    var result = await useCase.ExecuteAsync(id);

    if(result.HasError())
        return result.Error!.ToHttpResult();

    return TypedResults.Ok(result.Value);
}