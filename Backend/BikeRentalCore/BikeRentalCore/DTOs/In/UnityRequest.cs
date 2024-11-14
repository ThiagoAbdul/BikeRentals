using BikeRentalCore.Entities;
using BikeRentalCore.Enums;
using System.Collections.Generic;

namespace BikeRentalCore.DTOs.In;

public class UnityRequest
{
    public Guid BikeId { get; set; }
    public Guid RentalPointId { get; set; }
    public int Amount { get; set; }

    public UnityRequest()
    {
        
    }

    public List<Unity> ToEntities()
    {
        List<Unity> units = new List<Unity>();
        for (int i = 0; i < Amount; i++)
        {
            units.Add(new Unity()
            {
                BikeId = BikeId,
                RentalPointId = RentalPointId
            });
        }
        return units;


    }
}
