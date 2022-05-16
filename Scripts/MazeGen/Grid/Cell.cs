using System;
using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class Cell : IComparable<Cell>, IComparable {
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

        public int CompareTo(Cell other) {
            if (ReferenceEquals(this, other)) {
                return 0;
            }

            if (ReferenceEquals(null, other)) {
                return 1;
            }

            var xComparison = X.CompareTo(other.X);
            if (xComparison != 0) {
                return xComparison;
            }

            return Y.CompareTo(other.Y);
        }

        public int CompareTo(object obj) {
            if (ReferenceEquals(null, obj)) {
                return 1;
            }

            if (ReferenceEquals(this, obj)) {
                return 0;
            }

            return obj is Cell other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Cell)}");
        }
    }
}
