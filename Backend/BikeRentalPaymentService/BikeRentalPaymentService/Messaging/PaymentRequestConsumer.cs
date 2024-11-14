using BikeRentalPaymentService.Message;
using BikeRentalPaymentService.UseCases;
using MassTransit;
using Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace BikeRentalPaymentService.Messaging;

public class PaymentRequestConsumer(DoPaymentUseCase useCase, 
                                    ILogger<PaymentRequestConsumer> logger) 
                                        : IConsumer<PaymentRequest>
{
    public async Task Consume(ConsumeContext<PaymentRequest> context)
    {
        await useCase.ExecuteAsync(context.Message);
        logger.LogInformation("Processed");

    }
}
