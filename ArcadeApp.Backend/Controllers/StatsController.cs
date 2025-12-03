using ArcadeApp.Core.Models;
using ArcadeApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArcadeApp.Backend.Controllers;

[ApiController]
[Route("api/stats")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<IReadOnlyList<GameStat>>> GetStats(Guid userId, CancellationToken cancellationToken)
    {
        var stats = await _statsService.GetStatsAsync(userId, cancellationToken);
        return Ok(stats);
    }
}
