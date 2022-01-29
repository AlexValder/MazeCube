using Godot;
using MazeCube.Scripts.MazeGen.MazeAlgo;

namespace MazeCube.Scripts.MazeGen.Mesh {
    public class Printer : Node {
        public void Print() {
            var grid = new Grid.Grid(8, 8);
            new RecursiveBacktracker().Project(grid);
            grid.DrawInConsole();
        }
    }
}
