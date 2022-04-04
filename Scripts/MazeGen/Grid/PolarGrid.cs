using System;
using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class PolarGrid : CircularGrid {

        public int Rings => Width;
        public int Segments => Height;

        public PolarGrid(int ringHeight, int cellsInRing) : base(rows: cellsInRing, columns: ringHeight) { }
    }
}
