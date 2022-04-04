using System.Drawing;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    internal static partial class GraphicsExtensions {
        private abstract class BasePainter {
            protected readonly Graphics G;
            protected readonly RectangleF Rect;

            protected BasePainter(Graphics g, RectangleF rect) {
                G    = g;
                Rect = rect;
            }

            public abstract void NoMaze(Brush brush);
            public abstract void PrepareRooms(Brush fgBrush, Brush bgBrush, Grid.Grid grid);
        }
    }
}
