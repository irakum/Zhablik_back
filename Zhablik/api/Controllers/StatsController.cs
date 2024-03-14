using Microsoft.AspNetCore.Mvc;
using Zhablik.Managers;
using Zhablik.Models;

namespace Zhablik.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatsController: ControllerBase
{
    private readonly StatisticsManager _statManager;

    public StatsController(StatisticsManager statManager)
    {
        _statManager = statManager;
    }
    
    [HttpGet("dailystats")]
    public IActionResult GetDailyStats([FromQuery] string username)
    {
        Dictionary<string, int> dailyStats = _statManager.GetDailyStats(username);

        return Ok(dailyStats);
    }
    
    [HttpGet("weeklystats")]
    public IActionResult GetWeeklyStats([FromQuery] string username)
    {
        Dictionary<string, int> weeklyStatistics = _statManager.GetWeeklyStatistics(username);

        return Ok(weeklyStatistics);
    }
    
    [HttpGet("monthlystats")]
    public IActionResult GetMonthlyStats([FromQuery] string username)
    {
        Dictionary<string, int> monthlyStatistics = _statManager.GetMonthlyStatistics(username);

        return Ok(monthlyStatistics);
    }
}