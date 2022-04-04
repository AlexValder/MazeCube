namespace MazeCube.Scripts.MazeGen.Grid {
    public class PolarCell : Cell {
        public int Row => X;
        public int Cell => Y;

        public PolarCell(int x, int y) : base(x, y) { }
    }
}
