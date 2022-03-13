using Godot;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public abstract class TexturePainter {
        protected abstract byte[] CreatePngImage(Grid.Grid grid);
        public Image CreateImage(Grid.Grid grid) {
            var image = new Godot.Image();
            image.LoadPngFromBuffer(CreatePngImage(grid));
            return image;
        }
        public ImageTexture CreateImageTexture(Grid.Grid grid) {
            var texture = new ImageTexture();
            texture.CreateFromImage(CreateImage(grid));
            return texture;
        }
    }
}
