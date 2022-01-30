namespace MazeCube.Scripts.MazeGen.Grid {
    public class CylinderGrid : Grid {
        public CylinderGrid(int rows, int columns) : base(rows, columns) { }

        public override Cell this[int row, int col] => base[row % Width, col % Height];
    }
}
