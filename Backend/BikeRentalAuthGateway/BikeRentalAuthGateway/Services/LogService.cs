using BikeRentalAuthGateway.Messaging;
using MassTransit;

namespace BikeRentalAuthGateway.Services;

public class LogService(IBus bus)
{

    public async Task AddLog(LogEvent logEvent)
    {
        ISendEndpoint endpoint = await bus.GetSendEndpoint(new Uri("queue:logs"));
        await endpoint.Send(logEvent);

    }
}
