using BikeRentalPaymentService.Models;

namespace BikeRentalPaymentService.DTOs;

public class PaymentResponse(Payment payment)
{
    public Guid Id { get; set; } = payment.Id;
    public string ObjectId { get; set; } = payment.ObjectId;
    public string UserId { get; set; } = payment.UserId;
    public PaymentStatus Status { get; set; } = payment.Status;
    public PaymentMethod PaymentMethod { get; set; } = payment.PaymentMethod;
    public DateTime CreatedAt { get; set; } = payment.CreatedAt;
    public DateTime? FinishedAt { get; set; } = payment?.FinishedAt;
}
