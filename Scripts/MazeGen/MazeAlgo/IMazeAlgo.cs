using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.MazeAlgo {
    public interface IMazeAlgo {
        void Project(Grid.Grid grid, Cell startAt = null);
    }
}