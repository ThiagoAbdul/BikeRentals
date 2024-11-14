namespace BikeRentalCore.Entities;

public class Brand : EntityBase
{
    public string Name { get; set; }
    public virtual List<Bike> Bikes { get; set; } = [];

    public Brand()
    {
        
    }

    public Brand(string name)
    {
        Name = name;  
    }
}
