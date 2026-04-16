using GovDigitalApp.Application.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovDigitalApp.API.Controllers;

[Authorize]
public class IdentityController : BaseApiController
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet("proof")]
    public async Task<IActionResult> GetIdentityProof()
    {
        try
        {
            var proof = await _identityService.GetIdentityProofAsync(CurrentUserId);
            return Ok(proof);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("work-proof/share-code")]
    public async Task<IActionResult> GenerateWorkProofShareCode()
    {
        var code = await _identityService.GenerateWorkProofShareCodeAsync(CurrentUserId);
        return Ok(new { shareCode = code });
    }
}
