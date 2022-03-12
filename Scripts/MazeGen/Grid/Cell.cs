using System;
using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class Cell {
        public int X { get; }
        public int Y { get; }
        public Directions Directions { get; internal set; }

        public bool Up => (Directions & Directions.Up) == Directions.Up;
        public bool Right => (Directions & Directions.Right) == Directions.Right;
        public bool Left => (Directions & Directions.Left) == Directions.Left;
        public bool Down => (Directions & Directions.Down) == Directions.Down;

        public Cell(int x, int y) {
            X = x;
            Y = y;
        }
        public override string ToString() => $"[{X}, {Y}]";
    }
}
