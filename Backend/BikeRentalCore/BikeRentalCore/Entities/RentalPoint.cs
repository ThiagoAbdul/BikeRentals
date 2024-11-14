namespace BikeRentalCore.Entities;

public class RentalPoint: EntityBase
{
    public string UnityName { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public int AddressNumber { get; set; }
    public string? Complement{ get; set; }
    public virtual List<Unity> Units { get; set; } = [];
}
