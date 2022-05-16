using System.Drawing;
using System.IO;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class CubePainter : TexturePainter {
        public CubePainter(int size) : base(size) { }

        protected override byte[] CreatePngImage(Grid.Grid grid) {
            using var image = new Bitmap(Side, Side);
            using var g     = Graphics.FromImage(image);

            var rect = new Rectangle(0, 0, Side, Side);
            g.PaintCubeMaze(rect, (CubeGrid)grid, FgColor, BgColor);

            using var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
