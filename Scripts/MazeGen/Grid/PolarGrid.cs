using System;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class PolarGrid : Grid {
        public PolarGrid(int ringHeight, int cellsInRing) : base(rows: ringHeight, columns: cellsInRing) {
        }
    }
}
