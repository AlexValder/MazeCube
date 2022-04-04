using System.Drawing;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static partial class GraphicsExtensions {
        private class SidePainter : BasePainter {
            public float H { get; }
            public float V { get; }

            public SidePainter(Graphics graphics, RectangleF rect, float h, float v) : base(graphics, rect) {
                H     = h;
                V     = v;
            }

            public override void PrepareRooms(Brush fgBrush, Brush bgBrush, Grid.Grid grid) {
                for (var i = 0; i < grid.Height; ++i)
                for (var j = 0; j < grid.Width; ++j) {
                    G.FillRectangle(
                        bgBrush,
                        x: Rect.X + H * i,
                        y: Rect.Y + V * j,
                        width: H,
                        height: V
                    );
                    G.FillRectangle(
                        fgBrush,
                        x: Rect.X + H * i + H / 4f,
                        y: Rect.Y + V * j + V / 4f,
                        width: H / 2f,
                        height: V / 2f
                    );
                }
            }

            public void DrawUp(Pen pen, int i, int j) => G.DrawLine(
                pen,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j + V / 2f,
                Rect.X + H * i - H / 2f,
                Rect.Y + V * j + V / 2f
            );

            public void DrawDown(Pen pen, int i, int j) => G.DrawLine(
                pen,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j + V / 2f,
                Rect.X + H * i + H * 1.5f,
                Rect.Y + V * j + V / 2f
            );

            public void DrawRight(Pen pen, int i, int j) => G.DrawLine(
                pen,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j + V / 2f,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j + V * 1.5f
            );

            public void DrawLeft(Pen pen, int i, int j) => G.DrawLine(
                pen,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j + V / 2f,
                Rect.X + H * i + H / 2f,
                Rect.Y + V * j - V / 2f
            );

            public override void NoMaze(Brush brush) {
                G.FillRectangle(brush, Rect);
            }
        }
    }
}
