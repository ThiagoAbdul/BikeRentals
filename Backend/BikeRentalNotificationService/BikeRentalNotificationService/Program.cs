using Amazon.SimpleEmailV2.Model;
using BikeRentalNotificationService;
using MassTransit;
using Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IEmailClient, EmailClient>();


builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.SetKebabCaseEndpointNameFormatter();

    busConfiguration.AddConsumer<AuthorizedRentalConsumer>();

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

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

//var scope = host.Services.CreateScope();
//IEmailClient emailClient = scope.ServiceProvider.GetRequiredService<IEmailClient>();

//emailClient.SendAsync("noreply@thiagoabdul.com.br", "thiagoabdul10@gmail.com", "Thiago", "Teste").Wait();
host.Run();
