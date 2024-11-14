using BikeRentalCore.DTOs.Out;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalPointController(RentalPointService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentalPoints = await service.GetAllRentalPoints();
            IEnumerable<RentalPointMinimalResponse> response = rentalPoints
                .Select(x => new RentalPointMinimalResponse(x))
                .ToList(); 

            return Ok(response);
        }
    }
}
