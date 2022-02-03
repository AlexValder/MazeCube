using System;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.MazeAlgo {
    public abstract class RandomMaze : IMazeAlgo {
        protected Random Random { get; }
        protected int? Seed { get; }

        protected RandomMaze(int? seed = null) {
            Seed   = seed;
            Random = seed == null ? new Random() : new Random(seed.Value);
        }

        public abstract void Project(Grid.Grid grid, Cell startAt = null);
    }
}
