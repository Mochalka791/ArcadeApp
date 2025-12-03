using ArcadeApp.Core.Models.Enums;

namespace ArcadeApp.Core.Models;

public class GameStat
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public GameType Game { get; set; }
    public int Score { get; set; }
    public DateTimeOffset RecordedAt { get; set; } = DateTimeOffset.UtcNow;
}
