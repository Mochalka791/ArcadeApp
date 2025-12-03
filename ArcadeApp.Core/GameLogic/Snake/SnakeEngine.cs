namespace ArcadeApp.Core.GameLogic.Snake;

public class SnakeEngine
{
    public int Score { get; private set; }
    public void Tick() => Score++;
}
