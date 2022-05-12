using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class PlainPainter : TexturePainter {
        public PlainPainter(int side = 500) : base(side) {
        }

        protected override byte[] CreatePngImage(Grid.Grid grid) {
            using var image = new Bitmap(Side, Side);
            using (var g = Graphics.FromImage(image)) {
                var allRect = new Rectangle(0, 0, 500, 500);
                g.PaintRectMaze(allRect, grid, Color.White, Color.Black);
            }

            using (var stream = new MemoryStream()) {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
