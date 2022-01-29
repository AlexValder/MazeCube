using System.Drawing;
using System.IO;
using Godot;
using MazeCube.Scripts.MazeGen.Grid;
using Color = System.Drawing.Color;
using Image = Godot.Image;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class CylinderPainter : TexturePainter {
        public int Side { get; }
        public Color ErrorColor { get; set; } = Color.Red;
        public Color BgColor { get; set; } = Color.Black;
        public Color FgColor { get; set; } = Color.White;

        public CylinderPainter(int side) {
            Side = side;
        }

        private byte[] CreatePngImage(Grid.Grid grid) {
            using (var image = new Bitmap(Side, Side)) {
                PaintTexture(image, grid);

                using (var stream = new MemoryStream())
                {
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        private void PaintTexture(Bitmap image, Grid.Grid grid) {
            var allImage      = new Rectangle(0, 0, Side, Side);
            var sideRectangle = new Rectangle(0, 0, Side, Side / 2);
            var topCircle     = new Rectangle(0, Side / 2, Side / 2, Side / 2);
            var bottomCircle  = new Rectangle(Side / 2, Side / 2, Side / 2, Side / 2);

            using (var g = Graphics.FromImage(image)) {
                using (var errorBrush = new SolidBrush(ErrorColor)) {
                    g.FillRectangle(errorBrush, allImage);
                }

                // todo
                using (var topBrush = new SolidBrush(BgColor)) {
                    g.FillEllipse(topBrush, topCircle);
                }

                // todo
                using (var bottomBrush = new SolidBrush(BgColor)) {
                    g.FillEllipse(bottomBrush, bottomCircle);
                }

                // Side drawing
                g.PaintMaze(sideRectangle, grid, FgColor, BgColor);
            }
        }

        public override Image CreateImage(Grid.Grid grid) {
            var image = new Image();
            image.LoadPngFromBuffer(CreatePngImage(grid));
            return image;
        }

        public override ImageTexture CreateImageTexture(Grid.Grid grid) {
            var texture = new ImageTexture();
            texture.CreateFromImage(CreateImage(grid));
            return texture;
        }
    }
}
