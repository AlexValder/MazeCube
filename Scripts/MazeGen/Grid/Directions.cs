using System;

namespace MazeCube.Scripts.MazeGen.Grid {
    [Flags]
    public enum Directions {
        None = 0,
        Up = 1 << 0,
        Right = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3,
    }
}
