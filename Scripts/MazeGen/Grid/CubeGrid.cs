using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class CubeGrid : Grid {
        public CubeGrid(int rows, int columns) : base(rows * 2, columns * 3) {
            if (rows != columns) {
                throw new ArgumentException("Rows and Columns should be equal");
            }
        }

        /*
         *  +---+---+---+
         *  | 1 | 2 | 3 |
         *  +---+---+---+
         *  | 4 | 5 | 6 |
         *  +---+---+---+
         *  1) [1,2,3,4] are looped on horizontal
         *  2) [1,2,3,4] when led to top go to 5, with top of 1 going to top of 5
         *  3) [1,2,3,4] when led to bottom go to 6, with bottom of 1 going to top of 6
         */
        public override List<Cell> Neighbors(int x, int y) {
            var cells = new List<Cell>(4);
            // 1 or 4
            if (y >= 0 && y < Width / 3) {
                if (x < Height / 2) {
                    #region 1
                    // horizontal
                    if (y == 0) {
                        cells.Add(base[x + Height / 2, Width / 3 - 1]);
                        cells.Add(base[x, 1]);
                    } else {
                        cells.Add(base[x, y + 1]);
                        cells.Add(base[x, y - 1]);
                    }

                    // vertical
                    if (x == 0) {
                        cells.Add(base[1, y]);
                        cells.Add(base[Height / 2, 2 * Width / 3 - y - 1]);
                    } else if (x == Height / 2 - 1) {
                        cells.Add(base[Height / 2, y + 2 * Width / 3]);
                        cells.Add(base[x - 1, y]);
                    } else {
                        cells.Add(base[x + 1, y]);
                        cells.Add(base[x - 1, y]);
                    }
                    #endregion
                } else {
                    #region 4
                    // vertical
                    if (x == Height / 2) {
                        cells.Add(base[x + 1, y]);
                        cells.Add(base[Height - 1 - y, 2 * Width / 3 - 1]);
                    } else if (x == Height - 1) {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[Height - 1 - y, 2 * Width / 3]);
                    } else {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[x + 1, y]);
                    }

                    // horizontal
                    if (y == 0) {
                        cells.Add(base[x, 1]);
                        cells.Add(base[x - Height / 2, Width - 1]);
                    } else if (y == Width / 3 - 1) {
                        cells.Add(base[x, y - 1]);
                        cells.Add(base[x - Height / 2, 0]);
                    } else {
                        cells.Add(base[x, y - 1]);
                        cells.Add(base[x, y + 1]);
                    }
                    #endregion
                }
            }

            // 2 or 5
            if (y >= Width / 3 && y < 2 * Width / 3) {
                if (x < Height / 2) {
                    #region 2
                    // vertical
                    if (x == 0) {
                        cells.Add(base[1, y]);
                        cells.Add(base[y, Width / 3]);
                    } else if (x == Height / 2 - 1) {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[y, Width - 1]);
                    } else {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[x + 1, y]);
                    }

                    // horizontal
                    cells.Add(base[x, y - 1]);
                    cells.Add(base[x, y + 1]);
                    #endregion
                } else {
                    #region 5
                    // vertical
                    if (x == Height / 2) {
                        cells.Add(base[0, 2 * Width / 3 - y - 1]);
                        cells.Add(base[x + 1, y]);
                    } else if (x == Height - 1) {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[0, y + Width / 3]);
                    } else {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[x + 1, y]);
                    }

                    // horizontal
                    if (y == Width / 3) {
                        cells.Add(base[x, y + 1]);
                        cells.Add(base[0, x]);
                    } else if (y == 2 * Width / 3 - 1) {
                        cells.Add(base[Height / 2, Height - x - 1]);
                        cells.Add(base[x, y - 1]);
                    } else {
                        cells.Add(base[x, y - 1]);
                        cells.Add(base[x, y + 1]);
                    }
                    #endregion
                }
            }
            // 3 or 6
            if (y >= 2 * Width / 3 && y < Width) {
                if (x >= 0 && x < Height / 2) {
                    #region 3
                    // vertical
                    if (x == 0) {
                        cells.Add(base[1, y]);
                        cells.Add(base[Height - 1, y - Width / 3]);
                    } else if (x == Height / 2 - 1) {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[Height - 1, Width - y + 2 * Width / 3 - 1]);
                    } else {
                        cells.Add(base[x + 1, y]);
                        cells.Add(base[x - 1, y]);
                    }

                    // horizontal
                    cells.Add(base[x, y - 1]);
                    if (y == Width - 1) {
                        cells.Add(base[x + Height / 2, 0]);
                    } else {
                        cells.Add(base[x, y + 1]);
                    }
                    #endregion
                } else {
                    #region 6
                    // vertical
                    if (x == Height / 2) {
                        cells.Add(base[x - 1, y - 2 * Width / 3]);
                        cells.Add(base[x + 1, y]);
                    } else if (x == Height - 1) {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[x - Height / 2, Width - y - 1 + 2 * Width / 3]);
                    } else {
                        cells.Add(base[x - 1, y]);
                        cells.Add(base[x + 1, y]);
                    }

                    // horizontal
                    if (y == 2 * Width / 3) {
                        cells.Add(base[x, y + 1]);
                        cells.Add(base[Height - 1, 2 * Width / 3 - x - 1]);
                    } else if (y == Width - 1) {
                        cells.Add(base[x, y - 1]);
                        cells.Add(base[Height / 2 - 1, x]);
                    } else {
                        cells.Add(base[x, y - 1]);
                        cells.Add(base[x, y + 1]);
                    }
                    #endregion
                }
            }

            return cells;
        }

        protected override void SelectMutualPositions(Cell cell1, Cell cell2, Action<Cell, Directions> action) {
            if (
                IsSame(cell1, cell2) ||
                IsInside(cell1) && IsInside(cell2) ||
                IsFirstRow(cell1) && IsFirstRow(cell2)
            ) {
                if (cell2.X > cell1.X) {
                    action(cell1, Directions.Down);
                    action(cell2, Directions.Up);
                } else if (cell2.X < cell1.X) {
                    action(cell2, Directions.Down);
                    action(cell1, Directions.Up);
                }

                if (cell2.Y > cell1.Y) {
                    action(cell1, Directions.Right);
                    action(cell2, Directions.Left);
                } else if (cell2.Y < cell1.Y) {
                    action(cell2, Directions.Right);
                    action(cell1, Directions.Left);
                }
            }

            if (IsIn1(cell1) && IsIn4(cell2)) {
                action(cell1, Directions.Left);
                action(cell2, Directions.Right);
            } else if (IsIn1(cell2) && IsIn4(cell1)) {
                action(cell2, Directions.Left);
                action(cell1, Directions.Right);
            }

            if (IsIn3(cell1) && IsIn4(cell2)) {
                action(cell1, Directions.Right);
                action(cell2, Directions.Left);
            } else if (IsIn3(cell2) && IsIn4(cell1)) {
                action(cell2, Directions.Right);
                action(cell1, Directions.Left);
            }

            if (IsIn1(cell1) && IsIn5(cell2)) {
                action(cell1, Directions.Up);
                action(cell2, Directions.Up);
            } else if (IsIn1(cell2) && IsIn5(cell1)) {
                action(cell2, Directions.Up);
                action(cell1, Directions.Up);
            }

            if (IsIn2(cell1) && IsIn5(cell2)) {
                action(cell1, Directions.Up);
                action(cell2, Directions.Left);
            } else if (IsIn2(cell2) && IsIn5(cell1)) {
                action(cell2, Directions.Up);
                action(cell1, Directions.Left);
            }

            if (IsIn3(cell1) && IsIn5(cell2)) {
                action(cell1, Directions.Up);
                action(cell2, Directions.Down);
            } else if (IsIn3(cell2) && IsIn5(cell1)) {
                action(cell2, Directions.Up);
                action(cell1, Directions.Down);
            }

            if (IsIn4(cell1) && IsIn5(cell2)) {
                action(cell1, Directions.Up);
                action(cell2, Directions.Right);
            } else if (IsIn4(cell2) && IsIn5(cell1)) {
                action(cell2, Directions.Up);
                action(cell1, Directions.Right);
            }

            if (IsIn1(cell1) && IsIn6(cell2)) {
                action(cell1, Directions.Down);
                action(cell2, Directions.Up);
            } else if (IsIn1(cell2) && IsIn6(cell1)) {
                action(cell2, Directions.Down);
                action(cell1, Directions.Up);
            }

            if (IsIn2(cell1) && IsIn6(cell2)) {
                action(cell1, Directions.Down);
                action(cell2, Directions.Right);
            } else if (IsIn2(cell2) && IsIn6(cell1)) {
                action(cell2, Directions.Down);
                action(cell1, Directions.Right);
            }

            if (IsIn3(cell1) && IsIn6(cell2)) {
                action(cell1, Directions.Down);
                action(cell2, Directions.Down);
            } else if (IsIn3(cell1) && IsIn6(cell2)) {
                action(cell1, Directions.Down);
                action(cell2, Directions.Down);
            }

            if (IsIn4(cell1) && IsIn6(cell2)) {
                action(cell1, Directions.Down);
                action(cell2, Directions.Left);
            } else if (IsIn4(cell2) && IsIn6(cell1)) {
                action(cell2, Directions.Down);
                action(cell1, Directions.Left);
            }
        }

        private bool IsSame(Cell cell1, Cell cell2) {
            return
                IsIn1(cell1) && IsIn1(cell2) ||
                IsIn2(cell1) && IsIn2(cell2) ||
                IsIn3(cell1) && IsIn3(cell2) ||
                IsIn4(cell1) && IsIn4(cell2) ||
                IsIn5(cell1) && IsIn5(cell2) ||
                IsIn6(cell1) && IsIn6(cell2);
        }

        private bool IsIn1(Cell cell) {
            return cell.X < Height / 2 && cell.Y < Width / 3;
        }

        private bool IsIn2(Cell cell) {
            return cell.X < Height / 2 && cell.Y >= Width / 3 && cell.Y < 2 * Width / 3;
        }

        private bool IsIn3(Cell cell) {
            return cell.X < Height / 2 && cell.Y >= 2 * Width / 3;
        }

        private bool IsIn4(Cell cell) {
            return cell.X >= Height / 2 && cell.Y < Width / 3;
        }

        private bool IsIn5(Cell cell) {
            return cell.X >= Height / 2 && cell.Y >= Width / 3 && cell.Y < 2 * Width / 3;
        }

        private bool IsIn6(Cell cell) {
            return cell.X >= Height / 2 && cell.Y >= 2 * Width / 3;
        }

        private bool IsFirstRow(Cell cell) {
            return cell.X < Height / 2;
        }

        private bool IsInside(Cell cell) {
            return cell.X != 0 && cell.X != Height / 2 - 1 && cell.X != Height / 2 && cell.X != Height - 1 &&
                   cell.Y != 0 && cell.Y != Width / 3 - 1 && cell.Y != Width / 3 && cell.Y != 2 * Width / 3 - 1 &&
                   cell.Y != 2 * Width / 3 && cell.Y != Width - 1;
        }
    }
}
