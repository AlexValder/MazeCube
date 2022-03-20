using System;
using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class PolarGrid : CircularGrid {

        public int Rings => Height;
        public int Segments => Width;

        public PolarGrid(int ringHeight, int cellsInRing) : base(rows: cellsInRing, columns: ringHeight) { }
    }
}
