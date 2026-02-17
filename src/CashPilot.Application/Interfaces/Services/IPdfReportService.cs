using System.Security.Claims;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Services;

public interface IPdfReportService
{
    Task<byte[]> GeneratePdfReport(DateOnly startDate, DateOnly endDate, string userId);
}