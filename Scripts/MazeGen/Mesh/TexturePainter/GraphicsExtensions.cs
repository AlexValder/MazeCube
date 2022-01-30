using System.Drawing;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public static class GraphicsExtensions {
        public static void PaintMaze(
            this Graphics g,
            Rectangle rect,
            Grid.Grid grid,
            Color fgColor,
            Color bgColor
        ) {
            var h = rect.Width / grid.Width;
            var v = rect.Height / grid.Height;
            using (var bgPen = new Pen(bgColor)) {
                using (var fgPen = new Pen(fgColor)) {
                    // creating rooms
                    for (var i = 0; i < grid.Width; ++i) {
                        for (var j = 0; j < grid.Height; ++j) {
                            g.FillRectangle(
                                bgPen.Brush,
                                h * i,
                                v * j,
                                h,
                                v
                            );
                            g.FillRectangle(
                                fgPen.Brush,
                                h * i + h / 4f,
                                v * j + v / 4f,
                                h / 2f,
                                v / 2f
                            );
                        }
                    }

                    using (var penH = new Pen(fgColor, h / 2f)) {
                        using (var penV = new Pen(fgColor, v / 2f)) {
                            for (var i = 0; i < grid.Width; ++i) {
                                for (var j = 0; j < grid.Height; ++j) {
                                    var dirs = grid[i, j].Directions;

                                    // up: h less, v same
                                    if ((dirs & Directions.North) == Directions.North) {
                                        g.DrawLine(
                                            penV,
                                            h * i + h / 2f,
                                            v * j + v / 2f,
                                            h * i - h / 2f,
                                            v * j + v / 2f
                                        );
                                    }

                                    // to the right: v bigger, h same
                                    if ((dirs & Directions.East) == Directions.East) {
                                        g.DrawLine(
                                            penH,
                                            h * i + h / 2f,
                                            v * j + v / 2f,
                                            h * i + h / 2f,
                                            v * j + v * 1.5f
                                        );
                                    }

                                    // down: h bigger, v same
                                    if ((dirs & Directions.South) == Directions.South) {
                                        g.DrawLine(
                                            penV,
                                            h * i + h / 2f,
                                            v * j + v / 2f,
                                            h * i + h * 1.5f,
                                            v * j + v / 2f
                                        );
                                    }

                                    // to the left: v less, h same
                                    if ((dirs & Directions.West) == Directions.West) {
                                        g.DrawLine(
                                            penH,
                                            h * i + h / 2f,
                                            v * j + v / 2f,
                                            h * i + h / 2f,
                                            v * j - v / 2f
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
