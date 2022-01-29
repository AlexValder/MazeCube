using Godot;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public abstract class TexturePainter {
        public abstract Image CreateImage(Grid.Grid grid);
        public abstract ImageTexture CreateImageTexture(Grid.Grid grid);
    }
}
