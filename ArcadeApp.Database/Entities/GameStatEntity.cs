namespace ArcadeApp.Database.Entities;

public class GameStatEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int GameType { get; set; }
    public int Score { get; set; }
}
