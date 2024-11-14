using BikeRentalCore.DTOs.In;
using BikeRentalCore.DTOs.Out;
using BikeRentalCore.Entities;
using BikeRentalCore.Extensions;
using BikeRentalCore.Models;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BikeController(BikeService service) : ControllerBase
{

    [HttpPost]
    //[Authorize("InternalPolicy")]
    public async Task<IActionResult> Create([FromForm] BikeRequest body)
    {
        IFormFile image = body.Image;
        if (image == null)
            return BadRequest("Imagem não enviada");
        Bike bike = await service.CreateAsync(body.ToEntity(), image);
        BikeResponse response = new(bike);
        return Created("", response);

    }

    [HttpPost("rent")]
    public async Task<IActionResult> Rent([FromBody] RentRequest request)
    {

        string? userId = Request.UserId();

        if (userId is null)
            return Unauthorized();

        var result = await service.RentBikeAsync(userId, request);

        if (result.HasError())
            return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery(Name = "page")] int pageIndex = 1, [FromQuery] int size = 10)
    {
        Page<Bike> pageBikes = await service.GetBikesPaginated(pageIndex, size);
        Page<BikeResponse> response = pageBikes.Map(x => new BikeResponse(x));
    
        return Ok(response);
    }

}
