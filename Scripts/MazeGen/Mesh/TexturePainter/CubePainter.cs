using System.Drawing;
using System.IO;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class CubePainter : TexturePainter {
        public CubePainter(int size) : base(size) { }

        protected override byte[] CreatePngImage(Grid.Grid grid) {
            using var image = new Bitmap(Side, Side);
            using var g     = Graphics.FromImage(image);

            var rect = new Rectangle(0, 0, Side, Side);
            g.PaintRectMaze(rect, grid, FgColor, BgColor);

            using var brush1 = new SolidBrush(Color.FromArgb(120, Color.Red));
            using var brush2 = new SolidBrush(Color.FromArgb(120, Color.Orange));
            using var brush3 = new SolidBrush(Color.FromArgb(120, Color.Yellow));
            using var brush4 = new SolidBrush(Color.FromArgb(120, Color.Green));
            using var brush5 = new SolidBrush(Color.FromArgb(120, Color.Blue));
            using var brush6 = new SolidBrush(Color.FromArgb(120, Color.Purple));

            var rect1 = new Rectangle(0, 0, Side / 3, Side / 2);
            var rect2 = new Rectangle(Side / 3, 0, Side / 3, Side / 2);
            var rect3 = new Rectangle(2 * Side / 3, 0, Side / 3, Side / 2);
            var rect4 = new Rectangle(0, Side / 2, Side / 3, Side / 2);
            var rect5 = new Rectangle(Side / 3, Side / 2, Side / 3, Side / 2);
            var rect6 = new Rectangle(2 * Side / 3, Side / 2, Side / 3, Side / 2);

            g.FillRectangle(brush1, rect1);
            g.FillRectangle(brush2, rect2);
            g.FillRectangle(brush3, rect3);
            g.FillRectangle(brush4, rect4);
            g.FillRectangle(brush5, rect5);
            g.FillRectangle(brush6, rect6);

            using var stream = new MemoryStream();
            Save(image, "TEST/cube.png");
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
