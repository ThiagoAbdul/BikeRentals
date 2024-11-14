using BikeRentalCore.DTOs;
using BikeRentalCore.DTOs.Out;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(BrandService brandService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await brandService.GetAllBrands();
            var response = brands.Select(x => new BrandResponse(x));
            return Ok(response);
        }
    }
}
