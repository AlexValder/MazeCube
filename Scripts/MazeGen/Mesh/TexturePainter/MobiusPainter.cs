using System.Drawing;
using System.IO;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class MobiusPainter : TexturePainter {
        public MobiusPainter(int size) : base(size) {
        }

        protected override byte[] CreatePngImage(Grid.Grid grid) {
            using var image = new Bitmap(Side, Side);
            using (var g = Graphics.FromImage(image)) {
                var rect1 = new RectangleF(0, 0, Side, Side / 4f);
                var rect2 = new RectangleF(0, Side / 2f, Side, Side / 4f);
                var rect3 = new RectangleF(0, 7 * Side / 8f, Side / 2f, Side / 8f);

                g.PaintRectMaze(rect1, grid, FgColor, BgColor);
                g.PaintRectMaze(rect2, grid, FgColor, BgColor);

                using var sideBrush = new SolidBrush(BgColor);
                g.FillRectangle(sideBrush, rect3);
            }

            using (var stream = new MemoryStream()) {
                image.RotateFlip(RotateFlipType.Rotate90FlipX);
                Save(image, "TEST/mobius.png");
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
