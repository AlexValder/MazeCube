using System.Drawing;
using Godot;
using Color = System.Drawing.Color;
using Image = Godot.Image;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class PlainPainter : TexturePainter {
        public override Image CreateImage(Grid.Grid grid) {
            var image = new Image();
            using (var bitmap = new Bitmap(500, 500))
            using (var g = Graphics.FromImage(bitmap)) {
                var allRect = new Rectangle(0, 0, 500, 500);
                g.PaintMaze(allRect, grid, Color.White, Color.Black);
            }

            return image;
        }

        public override ImageTexture CreateImageTexture(Grid.Grid grid) {
            var texture = new ImageTexture();
            texture.CreateFromImage(CreateImage(grid));
            return texture;
        }
    }
}
