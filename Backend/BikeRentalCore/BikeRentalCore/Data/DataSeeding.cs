using BikeRentalCore.Entities;

namespace BikeRentalCore.Data;

public  class DataSeeding(AppDbContext db)
{
    public void Execute()
    {
        List<Brand> brands = 
            [
                new("Trek"),
                new("Specialized"),
                new("Giant"),
                new("Cannondale"),
                new("Bianchi"),
                new("Scott"),
                new("Sanat Cruz"),
                new("Raleigh"),
                new("Colnago"),
                new("Kona"),
                new("Caloi"),
                new("Monark"),
                new("Soul Cycles"),
                new("Oggi"),
                new("Track & Bike"),
                new("KHS Brasil"),
                new("GTS"),
            ];

        if(!db.Bikes.Any())
        {
            db.AddRange(brands);
            db.SaveChanges();
        }

        List<RentalPoint> points =
            [
                new()
                {
                    UnityName = "Unidade A",
                    AddressNumber = 144,
                    ZipCode = "05023145"
                },
                new()
                {
                    UnityName = "Unidade B",
                    AddressNumber = 74,
                    ZipCode = "05023681"
                }
            ];

        if(!db.RentalPoints.Any())
        {
            db.AddRange(points);
            db.SaveChanges();
        }
    }

   
}
