using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class Grid {
        private readonly Cell[,] _innerGrid;

        public int Height => _innerGrid.GetLength(0);
        public int Width => _innerGrid.GetLength(1);
        public int Size => _innerGrid.Length;

        public Grid(int rows, int columns) {
            _innerGrid = PrepareGrid(rows, columns);
        }

        private Cell[,] PrepareGrid(int rows, int columns) {
            var grid = new Cell[rows, columns];
            for (var i = 0; i < rows; ++i) {
                for (var j = 0; j < columns; ++j) {
                    grid[i, j] = new Cell(i, j);
                }
            }

            return grid;
        }

        public virtual Cell this[int row, int col] {
            get {
                if (row < 0 || row >= _innerGrid.GetLength(0)) {
                    return null;
                }

                if (col < 0 || col >= _innerGrid.GetLength(1)) {
                    return null;
                }

                return _innerGrid[row, col];
            }
            protected set {
                if (
                    row < 0 || col < 0 ||
                    row >= _innerGrid.GetLength(0) || col >= _innerGrid.GetLength(1)
                ) {
                    return;
                }

                _innerGrid[row, col] = value;
            }
        }

        public Cell RandomCell(int? seed = null) {
            var rand = seed == null ? new Random() : new Random(seed.Value);
            return _innerGrid[rand.Next(0, Height), rand.Next(0, Width)];
        }

        public void Link(Cell cell1, Cell cell2) {
            SelectMutualPositions(cell1, cell2, (c, directions) => { c.Directions |= directions; });
        }

        public void Unlink(Cell cell1, Cell cell2) {
            SelectMutualPositions(cell1, cell2, (c, directions) => { c.Directions &= ~directions; });
        }

        protected virtual void SelectMutualPositions(Cell cell1, Cell cell2, Action<Cell, Directions> action) {
            if (cell1.Y == cell2.Y) {
                switch (cell2.X - cell1.X) {
                    case 1:
                        action(cell1, Directions.Down);
                        action(cell2, Directions.Up);
                        break;
                    case -1:
                        action(cell1, Directions.Up);
                        action(cell2, Directions.Down);
                        break;
                    default:
                        throw NotANeighbor(cell1, cell2);
                }
            } else if (cell1.X == cell2.X) {
                switch (cell2.Y - cell1.Y) {
                    case 1:
                        // cell1 is above cell2
                        action(cell1, Directions.Right);
                        action(cell2, Directions.Left);
                        break;
                    case -1:
                        // cell1 is below cell2
                        action(cell1, Directions.Left);
                        action(cell2, Directions.Right);
                        break;
                    default:
                        throw NotANeighbor(cell1, cell2);
                }
            } else {
                throw NotANeighbor(cell1, cell2);
            }
        }

        public virtual List<Cell> Neighbors(int x, int y) {
            var list = new List<Cell>(4);
            if (x < 0 || y < 0) return list;
            if (x >= Height || y >= Width) return list;

            if (x > 0) list.Add(this[x - 1, y]);
            if (y > 0) list.Add(this[x, y - 1]);
            if (x < Height - 1) list.Add(this[x + 1, y]);
            if (y < Width - 1) list.Add(this[x, y + 1]);
            return list;
        }

        protected ArgumentException NotANeighbor(Cell cell1, Cell cell2) {
            return new($"This cell is not a neighbor cell. (This {cell1}, cell is {cell2}");
        }

        [Conditional("DEBUG")]
        public virtual void DrawInConsole() {
            GD.Print("===========");
            DrawInConsole(this);
        }

        protected static void DrawInConsole(Grid grid) {
            for (var i = 0; i < grid.Height; ++i) {
                var chars = new List<char>(grid.Width);
                for (var j = 0; j < grid.Width; ++j) {
                    chars.Add(CellToChar(grid._innerGrid[i, j]));
                }

                GD.Print(string.Join("", chars));
            }

            char CellToChar(Cell cell) {
                switch (cell.Directions) {
                    case Directions.None:
                        return '▨';
                    case Directions.Up:
                        return '╨';
                    case Directions.Right:
                        return '╞';
                    case Directions.Down:
                        return '╥';
                    case Directions.Left:
                        return '╡';
                    case Directions.Up | Directions.Right:
                        return '╚';
                    case Directions.Up | Directions.Down:
                        return '║';
                    case Directions.Up | Directions.Left:
                        return '╝';
                    case Directions.Right | Directions.Down:
                        return '╔';
                    case Directions.Left | Directions.Down:
                        return '╗';
                    case Directions.Left | Directions.Right:
                        return '═';
                    case Directions.Right | Directions.Down | Directions.Left:
                        return '╦';
                    case Directions.Up | Directions.Down | Directions.Left:
                        return '╣';
                    case Directions.Up | Directions.Right | Directions.Left:
                        return '╩';
                    case Directions.Up | Directions.Right | Directions.Down:
                        return '╠';
                    case Directions.Up | Directions.Right | Directions.Down | Directions.Left:
                        return '╬';
                    default:
                        return '╳';
                }
            }
        }
    }
}
