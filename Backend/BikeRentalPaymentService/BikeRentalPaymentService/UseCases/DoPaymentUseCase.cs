using BikeRentalPaymentService.Data;
using BikeRentalPaymentService.DTOs;
using BikeRentalPaymentService.Messaging;
using BikeRentalPaymentService.Models;
using Messaging;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalPaymentService.UseCases;

public class DoPaymentUseCase(IHttpClientFactory httpClientFactory, 
                              Settings settings,
                              PaymentRepository repository)
{
    public async Task<Result> ExecuteAsync(PaymentRequest request)
    {

        // 1 - Salvar no banco
        Payment payment = Payment.CreatePendingPayment(
            bikeId: request.ObjectId,
            userId: request.UserId,
            paymentMethod: request.PaymentMethod
        );
        var transaction = await repository.BeginTransaction();
        await repository.CreateAsync(payment);

        // 2 - Enviar para o gateway

        HttpClient httpClient = httpClientFactory.CreateClient();

        PaymentGatewayRequest gatewayRequest = new(
            request.PaymentData,
            request.PaymentMethod,
            settings.HostAddress + "/api/v1/payment-confirmation/" + payment.Id
        );

        HttpResponseMessage httpResponseMessage = await httpClient
            .PostAsJsonAsync(settings.PaymentGatewayUrl, gatewayRequest);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await transaction.CommitAsync();

            return Result.Empty();
        }
        await transaction.RollbackAsync();

        string message = await httpResponseMessage.Content.ReadAsStringAsync();
        return Result.SummaryError(message);

    }


}
