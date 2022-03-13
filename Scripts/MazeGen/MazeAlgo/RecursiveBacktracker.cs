using System;
using System.Collections.Generic;
using System.Linq;
using MazeCube.Scripts.MazeGen.Grid;
using Serilog;

namespace MazeCube.Scripts.MazeGen.MazeAlgo {
    public class RecursiveBacktracker : RandomMaze {
        public RecursiveBacktracker(int? seed = null) : base(seed) { }

        public override void Project(Grid.Grid grid, Cell startAt = null) {
            var stack = new List<Cell> {
                startAt ?? grid.RandomCell(Seed)
            };

            while (stack.Count > 0) {
                var current   = stack[stack.Count - 1];
                var neighbors = grid
                    .Neighbors(current.X, current.Y)
                    .Where(c => c.Directions == Directions.None)
                    .ToList();

                if (!neighbors.Any()) {
                    stack.RemoveAt(stack.Count - 1);
                } else {
                    var neighbor = neighbors[Random.Next(neighbors.Count)];
                    grid.Link(current, neighbor);
                    stack.Add(neighbor);
                }
            }
        }
    }
}
