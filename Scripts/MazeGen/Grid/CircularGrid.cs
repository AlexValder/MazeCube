using System;
using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    /// <summary>
    /// Base class for grids that loop on one of the axis
    /// </summary>
    /// <remarks>
    /// The looping axis here is Height
    /// </remarks>
    public abstract class CircularGrid : Grid {
        protected CircularGrid(int rows, int columns) : base(rows, columns) { }

        public override Cell this[int row, int col] {
            get {
                if (row < 0) {
                    return base[(Height + row) % Height, col];
                }

                return base[row % Height, col];
            }
        }

        protected override void SelectMutualPositions(Cell cell1, Cell cell2, Action<Cell, Directions> action) {
            if (cell1.X == cell2.X) {
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
            } else if (cell1.Y == cell2.Y) {
                if (cell1.X == Height - 1 && cell2.X == 0) {
                    action(cell1, Directions.Down);
                    action(cell2, Directions.Up);
                } else if (cell1.X == 0 && cell2.X == Height - 1) {
                    action(cell1, Directions.Up);
                    action(cell2, Directions.Down);
                } else {
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
                }
            } else {
                throw NotANeighbor(cell1, cell2);
            }
        }

        public override List<Cell> Neighbors(int x, int y) {
            var list = new List<Cell>(4);
            if (y < 0) return list;
            if (y >= Width) return list;

            list.Add(this[x - 1, y]);
            if (y > 0) list.Add(this[x, y - 1]);
            list.Add(this[x + 1, y]);
            if (y < Width - 1) list.Add(this[x, y + 1]);
            return list;
        }
    }
}
