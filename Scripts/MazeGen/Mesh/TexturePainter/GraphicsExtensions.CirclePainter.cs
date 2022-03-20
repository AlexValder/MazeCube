using System;
using System.Drawing;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static partial class GraphicsExtensions {
        private class CirclePainter : BasePainter {
            private const int ACCURACY = 5;

            public double Theta { get; }
            public float Radius { get; }
            public float RingHeight { get; }
            public PointF Center { get; }

            public CirclePainter(
                Graphics g,
                RectangleF rect,
                double theta = 0.0,
                float radius = 0f,
                float ringH = 0f
            ) : base(g, rect) {
                Theta      = theta;
                Radius     = radius;
                RingHeight = ringH;
                Center     = new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
            }

            public override void PrepareRooms(Brush fgBrush, Brush bgBrush, Grid.Grid grid) {
                if (grid is not PolarGrid pgrid) {
                    throw new ArgumentException($"{nameof(grid)} should be of type PolarGrid");
                }

                G.FillEllipse(fgBrush, Rect);

                using var bgPen = new Pen(bgBrush, RingHeight / 3f);

                for (var i = 0; i < pgrid.Segments; ++i) {
                    var point = new Point(
                        x: (int)(Math.Cos(Theta * (i + 0.5)) * Radius + Center.X),
                        y: (int)(Math.Sin(Theta * (i + 0.5)) * Radius + Center.Y)
                    );
                    G.DrawLine(bgPen, Center, point);
                }

                for (var j = 0; j < pgrid.Rings; ++j) {
                    G.DrawEllipse(
                        bgPen,
                        new RectangleF(
                            x: Rect.X + RingHeight * j,
                            y: Rect.Y + RingHeight * j,
                            width: Rect.Width - 2 * RingHeight * j,
                            height: Rect.Height - 2 * RingHeight * j
                        )
                    );
                }
            }

            public override void NoMaze(Brush fgBrush) => G.FillEllipse(fgBrush, Rect);

            public void DrawClockwise(Pen pen, int i, int j) => EraseSegmentWall(
                pen: pen,
                i: i,
                radius: RingHeight * j + RingHeight / 2f
            );

            public void DrawCounterClockwise(Pen pen, int i, int j) => EraseSegmentWall(
                pen: pen,
                i: i - 1,
                radius: RingHeight * j + RingHeight / 2f
            );

            private void EraseSegmentWall(Pen pen, int i, float radius) {
                var x1 = Center.X + radius * (float)Math.Cos(Theta * i);
                var y1 = Center.Y + radius * (float)Math.Sin(Theta * i);
                var x2 = Center.X + radius * (float)Math.Cos(Theta * (i + 1.0 / 3));
                var y2 = Center.Y + radius * (float)Math.Sin(Theta * (i + 1.0 / 3));
                var x3 = Center.X + radius * (float)Math.Cos(Theta * (i + 2.0 / 3));
                var y3 = Center.Y + radius * (float)Math.Sin(Theta * (i + 2.0 / 3));
                var x4 = Center.X + radius * (float)Math.Cos(Theta * (i + 1.0));
                var y4 = Center.Y + radius * (float)Math.Sin(Theta * (i + 1.0));

                G.DrawCurve(pen, new PointF[] {
                    new(x1, y1),
                    new(x2, y2),
                    new(x3, y3),
                    new(x4, y4),
                });
            }

            public void DrawToCenter(Pen pen, int i, int j) => EraseRingPart(pen, i, RingHeight * j);

            public void DrawFromCenter(Pen pen, int i, int j) => EraseRingPart(pen, i, RingHeight * (j + 1));

            private void EraseRingPart(Pen pen, int i, float radius) {
                var step   = Theta * 0.8f / ACCURACY;
                var points = new PointF[ACCURACY];
                for (var q = 0; q < ACCURACY; ++q) {
                    points[q] = new PointF(
                        x: (float)(Center.X + radius * Math.Cos(Theta * (i - 0.4 + step * q))),
                        y: (float)(Center.Y + radius * Math.Sin(Theta * (i - 0.4 + step * q)))
                    );
                }

                G.DrawCurve(pen, points);
            }
        }
    }
}
