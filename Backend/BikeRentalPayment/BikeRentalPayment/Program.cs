using BikeRentalPayment;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/api/health");

app.MapPost("/payment", DoPayment)
.WithName("Payment")
.WithOpenApi();

app.Run();

HttpClient httpClient = new();

static async Task DoPayment([FromBody] PaymentRequest payment, 
                            HttpContext context, 
                            IHttpClientFactory httpFactory)
{
    string callbackUrl = payment.CallbackUrl;
    if (string.IsNullOrEmpty(callbackUrl))
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.CompleteAsync();
        return;

    }
    context.Response.StatusCode = StatusCodes.Status204NoContent;
    await context.Response.CompleteAsync();

    Thread.Sleep(3_000);

    HttpClient httpClient = httpFactory.CreateClient();

    HttpResponseMessage response = await httpClient.PostAsync(callbackUrl, null);

}

