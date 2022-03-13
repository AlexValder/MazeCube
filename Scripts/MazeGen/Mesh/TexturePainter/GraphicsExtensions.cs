using System.Drawing;
using MazeCube.Scripts.MazeGen.Grid;
using Serilog;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static class GraphicsExtensions {
        private class Painter {
            public float H { get; }
            public float V { get; }
            private readonly Graphics _g;

            public Painter(Graphics graphics, float h, float v) {
                H  = h;
                V  = v;
                _g = graphics;
            }

            public void PrepareRooms(Brush fgBrush, Brush bgBrush, int width, int height) {
                for (var i = 0; i < width; ++i)
                for (var j = 0; j < height; ++j) {
                    _g.FillRectangle(bgBrush, H * i, V * j, H, V);
                    _g.FillRectangle(fgBrush, H * i + H / 4f, V * j + V / 4f, H / 2f, V / 2f);
                }
            }

            public void DrawUp(Pen pen, int i, int j) => _g.DrawLine(
                pen,
                H * i + H / 2f,
                V * j + V / 2f,
                H * i - H / 2f,
                V * j + V / 2f
            );

            public void DrawDown(Pen pen, int i, int j) => _g.DrawLine(
                pen,
                H * i + H / 2f,
                V * j + V / 2f,
                H * i + H * 1.5f,
                V * j + V / 2f
            );

            public void DrawRight(Pen pen, int i, int j) => _g.DrawLine(
                pen,
                H * i + H / 2f,
                V * j + V / 2f,
                H * i + H / 2f,
                V * j + V * 1.5f
            );

            public void DrawLeft(Pen pen, int i, int j) => _g.DrawLine(
                pen,
                H * i + H / 2f,
                V * j + V / 2f,
                H * i + H / 2f,
                V * j - V / 2f
            );
        }

        public static void PaintRectMaze(
            this Graphics g,
            Rectangle rect,
            Grid.Grid grid,
            Color fgColor,
            Color bgColor
        ) {
            if (grid.Size == 0) {
                using var brush = new SolidBrush(bgColor);
                g.FillRectangle(brush, rect);
                return;
            }

            var painter = new Painter(
                graphics: g,
                h: rect.Width * 1f / grid.Width,
                v: rect.Height * 1f / grid.Height
            );

            using var bgPen = new Pen(bgColor);
            using var fgPen = new Pen(fgColor);
            // creating rooms
            painter.PrepareRooms(fgPen.Brush, bgPen.Brush, grid.Width, grid.Height);

            using var penH = new Pen(fgColor, painter.H / 2f);
            using var penV = new Pen(fgColor, painter.V / 2f);
            for (var i = 0; i < grid.Width; ++i)
            for (var j = 0; j < grid.Height; ++j) {
                var dirs = grid[i, j].Directions;

                // up: h less, v same
                if ((dirs & Directions.Up) == Directions.Up) {
                    painter.DrawUp(penV, i, j);
                }

                // to the right: v bigger, h same
                if ((dirs & Directions.Right) == Directions.Right) {
                    painter.DrawRight(penH, i, j);
                }

                // down: h bigger, v same
                if ((dirs & Directions.Down) == Directions.Down) {
                    painter.DrawDown(penV, i, j);
                }

                // to the left: v less, h same
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
            using var bgBrush = new SolidBrush(bgColor);
            g.FillEllipse(bgBrush, rect);

            if (grid == null || grid.Size == 0) {
                return;
            }

            using var fgBrush = new SolidBrush(fgColor);
            g.FillEllipse(fgBrush, new Rectangle(
                              x: rect.X + rect.Width / 4,
                              y: rect.Y + rect.Height / 4,
                              width: rect.Width / 2,
                              height: rect.Height / 2)
            );
        }
    }
}
