using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Expenses.Response;
using CashPilot.Domain.DTOs.Incomes.Response;
using QuestPDF.Fluent;

namespace CashPilot.Application.Services;

public class PdfReportService : IPdfReportService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IIncomeRepository _incomeRepository;
    private readonly IMapper _mapper;
    
    public PdfReportService(
        IExpenseRepository expenseRepository, 
        IIncomeRepository incomeRepository,
        IMapper mapper
        )
    {
        _expenseRepository = expenseRepository;
        _incomeRepository = incomeRepository;
        _mapper = mapper;
    }
    
    public async Task<byte[]> GeneratePdfReport(DateOnly startDate, DateOnly endDate, string userId)
    {
        var expensesList = await _expenseRepository.GetExpensesInTimeSpan(startDate, endDate, userId);
        var incomesList = await _incomeRepository.GetIncomesInTimeSpan(startDate, endDate, userId);

        var expensesMapped = _mapper.Map<List<ResponseExpenseDto>>(expensesList);
        var incomesMapped = _mapper.Map<List<ResponseIncomeDto>>(incomesList);

        return GeneratePdf(expensesMapped, incomesMapped);
    }
    
    

    private static byte[] GeneratePdf(List<ResponseExpenseDto> expensesList, List<ResponseIncomeDto> incomesList)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(10);
                
                page.Content().Column(column =>
                {
                    column.Item().Text("Financial Report");

                    column.Item().Text($"Expenses: {expensesList.Count}");
                    column.Item().Text($"incomes: {incomesList.Count}");
                    
                    foreach (var expense in expensesList)
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text(expense.Description);
                            row.ConstantItem(80).AlignRight().Text(expense.Amount.ToString("C"));
                        });
                    }

                    foreach (var income in incomesList)
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Text(income.Description);
                            row.ConstantItem(80).AlignRight().Text(income.Amount.ToString("C"));
                        });
                    }
                });
            });
        });
        
        return document.GeneratePdf();
    }
}