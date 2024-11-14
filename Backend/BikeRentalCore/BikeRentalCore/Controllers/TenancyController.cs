using BikeRentalCore.DTOs.Out;
using BikeRentalCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenancyController(TenancyService service) : ControllerBase
{
    [HttpGet("/{code}")]
    public async Task<IActionResult> GetTenancyCode([FromRoute] string code)
    {
        var tenancy = await service.GetByCodeRentalAsync(code);
        if(tenancy is null)
            return NotFound();
        return Ok(new TenancyResponse(tenancy));
    }

    [HttpPost("{id}/end-lease")]
    public async Task<IActionResult> EndLease([FromRoute] Guid id)
    {
        var result = await service.EndLease(id);

        if(result.HasError())
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}
