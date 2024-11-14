using BikeRentalCore.Enums;
using BikeRentalCore.Services;
using MassTransit;
using Message;
using Messaging;

namespace BikeRentalCore.Message;

public class PaymentConfirmationConsumer(TenancyService tenancyService, 
                                        IPublishEndpoint publishEndpoint) 
                                        : IConsumer<PaymentConfirmation>
{
    public async Task Consume(ConsumeContext<PaymentConfirmation> context)
    {
        PaymentConfirmation paymentConfirmation = context.Message;

        var status = paymentConfirmation.Approved
            ? ERentalStatus.Rented
            : ERentalStatus.Available;

        Guid tenancyId = Guid.Parse(paymentConfirmation.ObjectId);
        var tenancy = await tenancyService.ChangeRentalStatus(tenancyId, status);

        if (tenancy is null)
            return;

        if (paymentConfirmation.Approved)
        {
            AuthorizedRentalEvent rentalEvent = new(tenancy.UserId, tenancy.RentalCode!);

            await publishEndpoint.Publish(rentalEvent);

        }
        else
        {
            UnauthorizedRentalEvent rentalEvent = new(tenancy.UserId);

            await publishEndpoint.Publish(rentalEvent);
        }
        
    }
}



