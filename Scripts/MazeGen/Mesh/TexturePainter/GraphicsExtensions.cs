using System;
using System.Diagnostics;
using System.Drawing;
using MazeCube.Scripts.MazeGen.Grid;
using Color = System.Drawing.Color;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static partial class GraphicsExtensions {
        public static void PaintRectMaze(
            this Graphics g,
            RectangleF rect,
            Grid.Grid grid,
            Color fgColor,
            Color bgColor
        ) {
            var painter = new SidePainter(
                graphics: g,
                rect: rect,
                h: rect.Width * 1f / grid.Height,
                v: rect.Height * 1f / grid.Width
            );

            if (grid.Size == 0) {
                using var brush = new SolidBrush(bgColor);
                painter.NoMaze(brush);
                return;
            }

            using var bgPen = new Pen(bgColor);
            using var fgPen = new Pen(fgColor);
            // creating rooms
            painter.PrepareRooms(fgPen.Brush, bgPen.Brush, grid);

            using var penH = new Pen(fgColor, painter.H / 2f);
            using var penV = new Pen(fgColor, painter.V / 2f);
            for (var i = 0; i < grid.Height; ++i)
            for (var j = 0; j < grid.Width; ++j) {
                var dirs = grid[i, j].Directions;

                // up: h less, v same
                if ((dirs & Directions.Up) == Directions.Up) {
                    painter.DrawUp(penV, i, j);
                }

                // right: v bigger, h same
                if ((dirs & Directions.Right) == Directions.Right) {
                    painter.DrawRight(penH, i, j);
                }

                // down: h bigger, v same
                if ((dirs & Directions.Down) == Directions.Down) {
                    painter.DrawDown(penV, i, j);
                }

                // left: v less, h same
                if ((dirs & Directions.Left) == Directions.Left) {
                    painter.DrawLeft(penH, i, j);
                }
            }
        }

        public static void PaintPolarMaze(
            this Graphics g,
            Rectangle rect,
            PolarGrid grid,
            Color fgColor,
            Color bgColor
        ) {
            // todo: ellipse support?
            Debug.Assert(rect.Height == rect.Width);

            using var bgBrush = new SolidBrush(bgColor);
            using var fgBrush = new SolidBrush(fgColor);

            if (grid == null || grid.Size == 0) {
                new CirclePainter(g, rect).NoMaze(bgBrush);
                return;
            }

            var width = rect.Height / 6f / grid.Rings;
            var realRect = new RectangleF(
                x: rect.X + width / 3f,
                y: rect.Y + width / 3f,
                width: rect.Width - 2f * width / 3,
                height: rect.Height - 2f * width / 3
            );

            var radius = realRect.Height / 2f;
            var theta  = 2.0 * Math.PI / grid.Segments;
            var ringH  = radius / grid.Rings;

            var painter = new CirclePainter(g, realRect, theta, radius, ringH);
            painter.PrepareRooms(fgBrush, bgBrush, grid);

            using var sPen   = new Pen(fgBrush, width);
            using var rPen   = new Pen(fgBrush, 2 * width);
            using var cwPen  = new Pen(Color.Red, width);
            using var cwwPen = new Pen(Color.Chartreuse, width);
            using var inPen  = new Pen(Color.Blue, width);
            using var outPen = new Pen(Color.DeepPink, width);

            for (var i = 0; i < grid.Segments; ++i)
            for (var j = 0; j < grid.Rings; ++j) {
                var dirs = grid[i, j].Directions;

                if ((dirs & Directions.Up) == Directions.Up) {
                    painter.DrawCounterClockwise(rPen, i, j);
                }

                if ((dirs & Directions.Down) == Directions.Down) {
                    painter.DrawClockwise(rPen, i, j);
                }

                if ((dirs & Directions.Left) == Directions.Left) {
                    painter.DrawToCenter(sPen, i, j);
                }

                if ((dirs & Directions.Right) == Directions.Right) {
                    painter.DrawFromCenter(sPen, i, j);
                }
            }
        }
    }
}
