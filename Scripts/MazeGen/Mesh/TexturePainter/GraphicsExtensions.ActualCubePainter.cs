using System;
using System.Drawing;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static partial class GraphicsExtensions {
        private class ActualCubePainter : BasePainter {
            public float Alpha { get; }
            public ActualCubePainter(Graphics g, RectangleF rect, float alpha = 5) : base(g, rect) {
                Alpha = alpha;
            }

            public override void NoMaze(Brush brush) => G.FillRectangle(brush, Rect);

            public override void PrepareRooms(Brush fgBrush, Brush bgBrush, Grid.Grid grid) {
                G.FillRectangle(bgBrush, Rect);
                NoMaze(bgBrush);

                for (var i = 0; i < grid.Width; ++i)
                for (var j = 0; j < grid.Height; ++j) {
                    G.FillRectangle(
                        fgBrush,
                        i * Rect.Width / grid.Width + Rect.Width / (grid.Width * 2 * Alpha),
                        j * Rect.Height / grid.Height + Rect.Height / (grid.Height * 2 * Alpha),
                        Rect.Width / grid.Width - Rect.Width / (grid.Width * Alpha),
                        Rect.Height / grid.Height - Rect.Height / (grid.Height * Alpha)
                    );
                }
            }

            public void MakeCorridors(Brush brush, Cell cell, Grid.Grid grid) {
                if ((cell.Directions & Directions.Up) == Directions.Up) {
                    G.FillRectangle(
                        brush,
                        cell.Y * Rect.Width / grid.Width + Rect.Width / (grid.Width * 2 * Alpha),
                        cell.X * Rect.Height / grid.Height,
                        Rect.Width / grid.Width - Rect.Width / (grid.Width * Alpha),
                        Rect.Height / grid.Height - Rect.Height / (grid.Height * Alpha)
                    );
                }

                if ((cell.Directions & Directions.Right) == Directions.Right) {
                    G.FillRectangle(
                        brush,
                        cell.Y * Rect.Width / grid.Width + 2 * Rect.Width / (grid.Width * 2 * Alpha),
                        cell.X * Rect.Height / grid.Height + Rect.Height / (grid.Height * 2 * Alpha),
                        Rect.Width / grid.Width - Rect.Width / (grid.Width * Alpha),
                        Rect.Height / grid.Height - Rect.Height / (grid.Height * Alpha)
                    );
                }

                if ((cell.Directions & Directions.Left) == Directions.Left) {
                    G.FillRectangle(
                        brush,
                        cell.Y * Rect.Width / grid.Width,
                        cell.X * Rect.Height / grid.Height + Rect.Height / (grid.Height * 2 * Alpha),
                        Rect.Width / grid.Width - Rect.Width / (grid.Width * Alpha),
                        Rect.Height / grid.Height - Rect.Height / (grid.Height * Alpha)
                    );
                }
                if ((cell.Directions & Directions.Down) == Directions.Down) {
                    G.FillRectangle(
                        brush,
                        cell.Y * Rect.Width / grid.Width + Rect.Width / (grid.Width * 2 * Alpha),
                        cell.X * Rect.Height / grid.Height + 2 * Rect.Height / (grid.Height * 2 * Alpha),
                        Rect.Width / grid.Width - Rect.Width / (grid.Width * Alpha),
                        Rect.Height / grid.Height - Rect.Height / (grid.Height * Alpha)
                    );
                }
            }
        }

        public static void PaintCubeMaze(
            this Graphics g,
            RectangleF rect,
            CubeGrid grid,
            Color fgColor,
            Color bgColor
        ) {
            var painter = new ActualCubePainter(g, rect, 2);

            using var fgBrush = new SolidBrush(fgColor);
            using var bgBrush = new SolidBrush(bgColor);

            painter.PrepareRooms(fgBrush, bgBrush, grid);
            for (var i = 0; i < grid.Height; ++i)
            for (var j = 0; j < grid.Width; ++j) {
                painter.MakeCorridors(fgBrush, grid[i, j], grid);
            }
        }
    }
}
