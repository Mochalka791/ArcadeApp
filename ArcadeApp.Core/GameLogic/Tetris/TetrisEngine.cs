using System;

namespace ArcadeApp.Core.Games.Tetris;

public enum TetrominoType
{
    I, O, T, J, L, S, Z
}

public enum TetrisMoveResult
{
    Ok,
    Blocked,
    LineCleared,
    GameOver
}

public sealed class TetrisEngine
{
    public int Width { get; }
    public int Height { get; }

    // Board[y,x] – 0 = leer, >0 = TetrominoType+1
    public int[,] Board { get; }

    public TetrominoType? CurrentType { get; private set; }
    public int CurrentRow { get; private set; }
    public int CurrentCol { get; private set; }
    public int CurrentRotation { get; private set; }

    public bool IsGameOver { get; private set; }
    public int Score { get; private set; }
    public int LinesCleared { get; private set; }

    private readonly Random _rng = new();
    private int[,] _currentShape = new int[4, 4];

    public TetrisEngine(int width = 10, int height = 20)
    {
        Width = width;
        Height = height;
        Board = new int[height, width];
    }

    public void StartNewGame()
    {
        Array.Clear(Board, 0, Board.Length);
        IsGameOver = false;
        Score = 0;
        LinesCleared = 0;

        SpawnNewPiece();
    }

    public TetrisMoveResult Tick()
    {
        if (IsGameOver || CurrentType == null)
            return TetrisMoveResult.Blocked;

        // Versuch, einen Schritt nach unten zu gehen
        if (CanMoveTo(CurrentRow + 1, CurrentCol, CurrentRotation))
        {
            CurrentRow++;
            return TetrisMoveResult.Ok;
        }

        // Stück „verhärten“ / festsetzen
        LockCurrentPiece();
        var cleared = ClearFullLines();

        if (cleared > 0)
        {
            LinesCleared += cleared;
            Score += cleared switch
            {
                1 => 100,
                2 => 250,
                3 => 400,
                4 => 600,
                _ => cleared * 150
            };
        }

        // Neues Stück spawnen
        if (!SpawnNewPiece())
        {
            IsGameOver = true;
            return TetrisMoveResult.GameOver;
        }

        return cleared > 0 ? TetrisMoveResult.LineCleared : TetrisMoveResult.Ok;
    }

    public void MoveLeft()
    {
        if (CanMoveTo(CurrentRow, CurrentCol - 1, CurrentRotation))
            CurrentCol--;
    }

    public void MoveRight()
    {
        if (CanMoveTo(CurrentRow, CurrentCol + 1, CurrentRotation))
            CurrentCol++;
    }

    public void SoftDrop()
    {
        if (CanMoveTo(CurrentRow + 1, CurrentCol, CurrentRotation))
            CurrentRow++;
    }

    public void HardDrop()
    {
        while (CanMoveTo(CurrentRow + 1, CurrentCol, CurrentRotation))
        {
            CurrentRow++;
        }
        // Nach HardDrop tickst du meist einmal, um Lock + Clear zu machen
    }

    public void RotateClockwise()
    {
        var newRot = (CurrentRotation + 1) % 4;
        if (CanMoveTo(CurrentRow, CurrentCol, newRot))
        {
            CurrentRotation = newRot;
            _currentShape = GetShape(CurrentType!.Value, CurrentRotation);
        }
    }

    public void RotateCounterClockwise()
    {
        var newRot = (CurrentRotation + 3) % 4;
        if (CanMoveTo(CurrentRow, CurrentCol, newRot))
        {
            CurrentRotation = newRot;
            _currentShape = GetShape(CurrentType!.Value, CurrentRotation);
        }
    }

    private bool SpawnNewPiece()
    {
        var type = (TetrominoType)_rng.Next(0, 7);
        CurrentType = type;
        CurrentRotation = 0;
        _currentShape = GetShape(type, 0);

        CurrentRow = 0;
        CurrentCol = Width / 2 - 2; // grob zentriert

        if (!CanMoveTo(CurrentRow, CurrentCol, CurrentRotation))
        {
            // Kein Platz → Game Over
            return false;
        }

        return true;
    }

    private bool CanMoveTo(int newRow, int newCol, int rotation)
    {
        if (CurrentType is null)
            return false;

        var shape = GetShape(CurrentType.Value, rotation);
        var rows = shape.GetLength(0);
        var cols = shape.GetLength(1);

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (shape[r, c] == 0)
                    continue;

                var br = newRow + r;
                var bc = newCol + c;

                // Außerhalb des Spielfeldes?
                if (br < 0 || br >= Height || bc < 0 || bc >= Width)
                    return false;

                // Kollision mit bestehendem Block?
                if (Board[br, bc] != 0)
                    return false;
            }
        }

        return true;
    }

    private void LockCurrentPiece()
    {
        if (CurrentType is null)
            return;

        var shape = _currentShape;
        var rows = shape.GetLength(0);
        var cols = shape.GetLength(1);
        var value = (int)CurrentType.Value + 1; // 1–7

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (shape[r, c] == 0)
                    continue;

                var br = CurrentRow + r;
                var bc = CurrentCol + c;

                if (br >= 0 && br < Height && bc >= 0 && bc < Width)
                {
                    Board[br, bc] = value;
                }
            }
        }
    }

    private int ClearFullLines()
    {
        var cleared = 0;

        for (var r = Height - 1; r >= 0; r--)
        {
            var full = true;
            for (var c = 0; c < Width; c++)
            {
                if (Board[r, c] == 0)
                {
                    full = false;
                    break;
                }
            }

            if (!full)
                continue;

            // Zeile entfernen und alles darüber nach unten schieben
            for (var rr = r; rr > 0; rr--)
            {
                for (var c = 0; c < Width; c++)
                {
                    Board[rr, c] = Board[rr - 1, c];
                }
            }

            // oberste Zeile leeren
            for (var c = 0; c < Width; c++)
            {
                Board[0, c] = 0;
            }

            cleared++;
            r++; // gleiche Zeile erneut prüfen (weil wir alles nach unten gezogen haben)
        }

        return cleared;
    }

    private static int[,] GetShape(TetrominoType type, int rotation)
    {
        // Sehr simple Shapes; du kannst das später verfeinern/ändern
        // Jede Form ist 4x4
        return type switch
        {
            TetrominoType.I => Rotate(new int[,]
            {
                {0,0,0,0},
                {1,1,1,1},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            TetrominoType.O => new int[,]
            {
                {0,2,2,0},
                {0,2,2,0},
                {0,0,0,0},
                {0,0,0,0}
            },

            TetrominoType.T => Rotate(new int[,]
            {
                {0,3,0,0},
                {3,3,3,0},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            TetrominoType.J => Rotate(new int[,]
            {
                {4,0,0,0},
                {4,4,4,0},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            TetrominoType.L => Rotate(new int[,]
            {
                {0,0,5,0},
                {5,5,5,0},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            TetrominoType.S => Rotate(new int[,]
            {
                {0,6,6,0},
                {6,6,0,0},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            TetrominoType.Z => Rotate(new int[,]
            {
                {7,7,0,0},
                {0,7,7,0},
                {0,0,0,0},
                {0,0,0,0}
            }, rotation),

            _ => new int[4, 4]
        };
    }

    private static int[,] Rotate(int[,] shape, int times)
    {
        var result = (int[,])shape.Clone();
        for (var i = 0; i < times; i++)
        {
            result = Rotate90(result);
        }
        return result;
    }

    private static int[,] Rotate90(int[,] shape)
    {
        var size = shape.GetLength(0);
        var res = new int[size, size];

        for (var r = 0; r < size; r++)
        {
            for (var c = 0; c < size; c++)
            {
                res[c, size - 1 - r] = shape[r, c];
            }
        }

        return res;
    }
    public int GetDisplayCell(int row, int col)
    {
        var value = Board[row, col];

        if (CurrentType is null)
            return value;

        var shape = _currentShape;
        var rows = shape.GetLength(0);
        var cols = shape.GetLength(1);
        var pieceValue = (int)CurrentType.Value + 1;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                if (shape[r, c] == 0)
                    continue;

                var br = CurrentRow + r;
                var bc = CurrentCol + c;

                if (br == row && bc == col)
                {
                    return pieceValue;
                }
            }
        }

        return value;
    }

}
