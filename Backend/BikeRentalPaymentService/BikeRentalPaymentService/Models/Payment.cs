namespace BikeRentalPaymentService.Models;

public class Payment
{
    public Guid Id { get; set; }
    public required string ObjectId { get; set; }
    public required string UserId { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool Deleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

    private Payment()
    {
        
    }

    public void Approve()
    {
        Status = PaymentStatus.APPROVED;
        FinishedAt = DateTime.UtcNow;
    }

    public static Payment CreatePendingPayment(string bikeId, string userId, PaymentMethod paymentMethod)
    {
        return new Payment
        {
            ObjectId = bikeId,
            UserId = userId,
            Status = PaymentStatus.PENDING,
            PaymentMethod = paymentMethod,
            Deleted = false,
            CreatedAt = DateTime.UtcNow

        };
    }
}
