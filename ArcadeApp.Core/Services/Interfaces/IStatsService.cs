using ArcadeApp.Core.Models;

namespace ArcadeApp.Core.Services.Interfaces;

public interface IStatsService
{
    Task<IReadOnlyList<GameStat>> GetStatsAsync(Guid userId, CancellationToken cancellationToken = default);
}
