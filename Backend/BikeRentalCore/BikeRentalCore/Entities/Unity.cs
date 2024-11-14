using BikeRentalCore.Enums;

namespace BikeRentalCore.Entities;

public class Unity : EntityBase
{
    public Guid BikeId { get; set; }
    public Bike? Bike { get; set; }
    public string Sku { get; set; } = string.Empty;
    public Guid RentalPointId { get; set; }
    public RentalPoint? RentalPoint { get; set; }
    public ERentalStatus Status { get; set; }
    public virtual List<Tenancy> Tenancies { get; set; } = [];
}
