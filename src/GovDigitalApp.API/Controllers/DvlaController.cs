using GovDigitalApp.Application.Dvla;
using GovDigitalApp.Application.Dvla.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovDigitalApp.API.Controllers;

[Authorize]
public class DvlaController : BaseApiController
{
    private readonly IDvlaService _dvlaService;

    public DvlaController(IDvlaService dvlaService)
    {
        _dvlaService = dvlaService;
    }

    [HttpGet("vehicles")]
    public async Task<IActionResult> GetVehicles()
    {
        var vehicles = await _dvlaService.GetVehiclesAsync(CurrentUserId);
        return Ok(vehicles);
    }

    [HttpGet("vehicles/lookup/{plate}")]
    public async Task<IActionResult> LookupVehicle(string plate)
    {
        var vehicle = await _dvlaService.LookupVehicleAsync(plate);
        return Ok(vehicle);
    }

    [HttpPost("vehicles")]
    public async Task<IActionResult> AddVehicle([FromBody] AddVehicleRequest request)
    {
        var result = await _dvlaService.AddVehicleAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetVehicles), result);
    }

    [HttpGet("licence/status")]
    public async Task<IActionResult> GetLicenceStatus()
    {
        try
        {
            var status = await _dvlaService.GetDriverLicenceStatusAsync(CurrentUserId);
            return Ok(status);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("licence/renew")]
    public async Task<IActionResult> RenewLicence()
    {
        try
        {
            var result = await _dvlaService.RenewDriverLicenceAsync(CurrentUserId);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("penalty-points")]
    public async Task<IActionResult> GetPenaltyPoints()
    {
        var points = await _dvlaService.GetPenaltyPointsAsync(CurrentUserId);
        return Ok(points);
    }
}
