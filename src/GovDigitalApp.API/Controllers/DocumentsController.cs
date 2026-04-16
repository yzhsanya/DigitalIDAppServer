using GovDigitalApp.Application.Documents;
using GovDigitalApp.Application.Documents.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovDigitalApp.API.Controllers;

[Authorize]
public class DocumentsController : BaseApiController
{
    private readonly IDocumentsService _documentsService;

    public DocumentsController(IDocumentsService documentsService)
    {
        _documentsService = documentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDocuments()
    {
        var documents = await _documentsService.GetDocumentsAsync(CurrentUserId);
        return Ok(documents);
    }

    [HttpPost("passport")]
    public async Task<IActionResult> AddPassport([FromBody] AddPassportRequest request)
    {
        var result = await _documentsService.AddPassportAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("driving-licence")]
    public async Task<IActionResult> AddDrivingLicence([FromBody] AddDrivingLicenceRequest request)
    {
        var result = await _documentsService.AddDrivingLicenceAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("diploma")]
    public async Task<IActionResult> AddDiploma([FromBody] AddDiplomaRequest request)
    {
        var result = await _documentsService.AddDiplomaAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("birth-certificate")]
    public async Task<IActionResult> AddBirthCertificate([FromBody] AddBirthCertificateRequest request)
    {
        var result = await _documentsService.AddBirthCertificateAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("evisa")]
    public async Task<IActionResult> AddEVisa([FromBody] AddEVisaRequest request)
    {
        var result = await _documentsService.AddEVisaAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("travel-passport")]
    public async Task<IActionResult> AddPassportToTravelAbroad([FromBody] AddPassportToTravelAbroadRequest request)
    {
        var result = await _documentsService.AddPassportToTravelAbroadAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("resident-permit")]
    public async Task<IActionResult> AddResidentPermit([FromBody] AddResidentPermitRequest request)
    {
        var result = await _documentsService.AddResidentPermitAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpPost("marriage-certificate")]
    public async Task<IActionResult> AddMarriageCertificate([FromBody] AddMarriageCertificateRequest request)
    {
        var result = await _documentsService.AddMarriageCertificateAsync(CurrentUserId, request);
        return CreatedAtAction(nameof(GetDocuments), result);
    }

    [HttpDelete("{documentId:int}")]
    public async Task<IActionResult> DeleteDocument(int documentId)
    {
        try
        {
            await _documentsService.DeleteDocumentAsync(CurrentUserId, documentId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("reorder")]
    public async Task<IActionResult> ReorderDocuments([FromBody] List<int> orderedDocumentIds)
    {
        await _documentsService.ReorderDocumentsAsync(CurrentUserId, orderedDocumentIds);
        return NoContent();
    }
}
