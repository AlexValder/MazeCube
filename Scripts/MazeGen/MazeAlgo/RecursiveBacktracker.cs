using System;
using System.Collections.Generic;
using System.Linq;
using MazeCube.Scripts.MazeGen.Grid;

namespace MazeCube.Scripts.MazeGen.MazeAlgo {
    public class RecursiveBacktracker {
        private readonly Random _random;
        private readonly int? _seed;

        public RecursiveBacktracker(int? seed = null) {
            _seed   = seed;
            _random = seed == null ? new Random() : new Random(seed.Value);
        }

        public void Project(Grid.Grid grid, Cell startAt = null) {
            var stack = new List<Cell> {
                startAt ?? grid.RandomCell(_seed)
            };

            while (stack.Count > 0) {
                var current = stack[stack.Count - 1];
                var neighbors = current.Neighbors.Where(c => !c.Links.Any()).ToList();

                if (!neighbors.Any()) {
                    stack.RemoveAt(stack.Count - 1);
                } else {
                    var neighbor = neighbors[_random.Next(neighbors.Count)];
                    current.Link(neighbor);
                    stack.Add(neighbor);
                }
            }
        }
    }
}
