using CashPilot.Application.Interfaces.Services;

namespace CashPilot.Application.UseCases.PdfReport.Commands;

public class CreatePdfReportUseCase
{
    private readonly IPdfReportService _pdfReportService;

    public CreatePdfReportUseCase(IPdfReportService pdfReportService)
    {
        _pdfReportService = pdfReportService;
    }

    public async Task<byte[]> Execute(DateOnly startDate, DateOnly endDate, string userId)
    {
        return await _pdfReportService.GeneratePdfReport(startDate, endDate, userId);
    }
}