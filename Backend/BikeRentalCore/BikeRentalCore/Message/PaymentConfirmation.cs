namespace Messaging;

public class PaymentConfirmation
{
    public Guid PaymentId { get; set; }
    public string ObjectId { get; set; } = string.Empty;
    public bool Approved { get; set; }
    public DateTime Date { get; set; }
}