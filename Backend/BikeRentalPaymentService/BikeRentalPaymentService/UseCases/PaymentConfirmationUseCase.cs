using BikeRentalPaymentService.Data;
using BikeRentalPaymentService.DTOs.Errors;
using BikeRentalPaymentService.Models;
using MassTransit;
using MassTransit.Transports;
using Messaging;

namespace BikeRentalPaymentService.UseCases;

public class PaymentConfirmationUseCase(PaymentRepository repository, IPublishEndpoint publishEndpoint, ILogger<PaymentConfirmationUseCase> logger)
{
    public async Task<Result> ExecuteAsync(Guid paymentId)
    {
        Payment? payment = await repository.GetByIdAsync(paymentId);

        if (payment is null)
            return Result.ForError(new BadRequestErrorModel());

        payment.Approve();

        var transaction = await repository.BeginTransaction();
        await repository.UpdateAsync(payment);
        PaymentConfirmation paymentConfirmation = new(payment);

        try
        {
            await publishEndpoint.Publish(paymentConfirmation);

            await transaction.CommitAsync();
            await transaction.DisposeAsync();
            return Result.Empty();

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            return Result.ForError(new InternalErrorModel());
        }
    }
}
