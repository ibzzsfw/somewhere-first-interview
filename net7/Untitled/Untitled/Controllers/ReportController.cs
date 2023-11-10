using Untitled.Core.Services;

namespace Untitled.Controllers;

public class ReportController : IController
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public void Report()
    {
        _reportService.Report();
    }
}