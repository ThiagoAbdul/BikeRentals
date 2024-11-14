namespace BikeRentalCore.Entities;

public class EntityBase
{
    public Guid Id { get; set; }
    public bool Deleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
}
