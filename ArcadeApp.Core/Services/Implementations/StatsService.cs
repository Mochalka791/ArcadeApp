using ArcadeApp.Core.Models;
using ArcadeApp.Core.Models.Enums;
using ArcadeApp.Core.Services.Interfaces;

namespace ArcadeApp.Core.Services.Implementations;

public class StatsService : IStatsService
{
    public Task<IReadOnlyList<GameStat>> GetStatsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var stats = new List<GameStat>
        {
            new() { Game = GameType.Tetris, Score = 1200 },
            new() { Game = GameType.Snake, Score = 450 }
        };

        return Task.FromResult<IReadOnlyList<GameStat>>(stats);
    }
}
