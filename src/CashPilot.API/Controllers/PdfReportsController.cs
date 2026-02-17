using System.Security.Claims;
using CashPilot.Application.UseCases.PdfReport.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashPilot.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PdfReportsController : ControllerBase
{
    private readonly CreatePdfReportUseCase _pdfReportUseCase;

    public PdfReportsController(CreatePdfReportUseCase pdfReportUseCase)
    {
        _pdfReportUseCase = pdfReportUseCase;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePdfReport([FromQuery] DateOnly start, [FromQuery] DateOnly end)
    {
        var userId = GetUserId();
        
        if (userId is null) return BadRequest();
        
        byte[] fileBytes = await _pdfReportUseCase.Execute(start, end, userId);
        
        return File(fileBytes, "application/pdf", $"Report from {start} to {end}.pdf");
    }
    
    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}