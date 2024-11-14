using MassTransit;

namespace BikeRentalAuthGateway.Extensions;

public static class MessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busConfiguration =>
        {

            busConfiguration.UsingRabbitMq((ctx, cfg) =>
            {

                string host = Environment.GetEnvironmentVariable("MQ_HOST")
                ?? configuration["MessageBroker:Host"]!;

                string userName = Environment.GetEnvironmentVariable("MQ_USER")
                    ?? configuration["MessageBroker:UserName"]!;

                string password = Environment.GetEnvironmentVariable("MQ_PASSWORD")
                    ?? configuration["MessageBroker:Password"]!;

                Uri uri = new(host);
                cfg.Host(uri, h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(ctx);

            });
        });

        return services;
    }
}
