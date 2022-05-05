using Microsoft.AspNetCore.Mvc;

namespace AsyncProcessing.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("user_statistics")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserStatistics(UserStatisticsRequest request)
    {
        try
        {
            UserStatisticsResponse? result = await _reportService.GetUserStatistics(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("info/{queryId}")]
    [Produces("application/json")]
    public async Task<IActionResult> GetQueryInfo(Guid? queryId)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(queryId);
            QueryInfoResponse result = await _reportService.GetQueryInfo(queryId.Value);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
