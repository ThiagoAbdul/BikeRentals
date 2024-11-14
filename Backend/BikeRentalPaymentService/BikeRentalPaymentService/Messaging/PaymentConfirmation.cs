using BikeRentalPaymentService.Models;

namespace Messaging;

public class PaymentConfirmation(Payment payment)
{
    public Guid PaymentId { get; set; } = payment.Id;
    public string ObjectId { get; set; } = payment.ObjectId;
    public bool Approved { get; set; } = payment.Status == PaymentStatus.APPROVED;
    public DateTime Date { get; set; } = payment.FinishedAt!.Value;
}
