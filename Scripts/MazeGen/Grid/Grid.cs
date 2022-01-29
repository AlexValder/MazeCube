using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Godot;
using Color = System.Drawing.Color;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class Grid {
        private readonly Cell[,] _innerGrid;

        public int Width => _innerGrid.GetLength(0);
        public int Height => _innerGrid.GetLength(1);
        public int Size => _innerGrid.Length;

        public Grid(int rows, int columns) {
            _innerGrid = PrepareGrid(rows, columns);
            ConfigureCells();
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

        private void ConfigureCells() {
            foreach (var cell in _innerGrid) {
                var row = cell.Row;
                var col = cell.Column;

                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.West  = this[row, col - 1];
                cell.East  = this[row, col + 1];
            }
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
            return _innerGrid[rand.Next(0, Width), rand.Next(0, Height)];
        }

        [Conditional("DEBUG")]
        public void DrawInConsole() {
            for (var i = 0; i < Width; ++i) {
                var chars = new List<char>(Height);
                for (var j = 0; j < Height; ++j) {
                    chars.Add(CellToChar(_innerGrid[i, j]));
                }
                GD.Print(string.Join("", chars));
            }

            char CellToChar(Cell cell) {
                switch (cell.Directions) {
                    case Directions.None:
                        return '▨';
                    case Directions.North:
                        return '╨';
                    case Directions.East:
                        return '╞';
                    case Directions.South:
                        return '╥';
                    case Directions.West:
                        return '╡';
                    case Directions.North | Directions.East:
                        return '╚';
                    case Directions.North | Directions.South:
                        return '║';
                    case Directions.North | Directions.West:
                        return '╝';
                    case Directions.East | Directions.South:
                        return '╔';
                    case Directions.West | Directions.South:
                        return '╗';
                    case Directions.West | Directions.East:
                        return '═';
                    case Directions.East | Directions.South | Directions.West:
                        return '╦';
                    case Directions.North | Directions.South | Directions.West:
                        return '╣';
                    case Directions.North | Directions.East | Directions.West:
                        return '╩';
                    case Directions.North | Directions.East | Directions.South:
                        return '╠';
                    case Directions.North | Directions.East | Directions.South | Directions.West:
                        return '╬';
                    default:
                        return '╳';
                }
            }
        }
    }
}
