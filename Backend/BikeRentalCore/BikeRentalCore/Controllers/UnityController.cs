using BikeRentalCore.DTOs.In;
using BikeRentalCore.DTOs.Out;
using BikeRentalCore.Entities;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnityController(UnityService service) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] UnityRequest request)
    {
        if (request.Amount < 1)
            return BadRequest("Quantidade inválida");
        IEnumerable<Unity> units = await service.AddUnits(request.ToEntities());
        var response = units.Select(x => new UnityResponse(x));
        return Created("", response);
    }

    [HttpGet("sku/{sku}")]
    public async Task<IActionResult> GetBySku([FromRoute] string sku)
    {
        Unity? unity = await service.GetBySkuAsync(sku);

        if (unity is null)
            return NotFound();

        UnityMinimalResponse response = new(unity);
        return Ok(response);
    }

}
