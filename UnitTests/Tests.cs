using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MazeCube.Scripts.MazeGen.Grid;
using Xunit;

namespace UnitTests {
    /*
     *  +---+---+---+
     *  | 1 | 2 | 3 |
     *  +---+---+---+
     *  | 4 | 5 | 6 |
     *  +---+---+---+
     *  1) [1,2,3,4] are looped on horizontal
     *  2) [1,2,3,4] when led to top go to 5, with top of 1 going to top of 5
     *  3) [1,2,3,4] when led to bottom go to 6, with bottom of 1 going to top of 6
     *
     * ++=======+=======+=======++=======+=======+=======++=======+=======+=======++
     * || (0,0) | (0,1) | (0,2) || (0,3) | (0,4) | (0,5) || (0,6) | (0,7) | (0,8) ||
     * +--------+-------+-------++-------+-------+-------+--------+-------+-------++
     * || (1,0) | (1,1) | (1,2) || (1,3) | (1,4) | (1,5) || (1,6) | (1,7) | (1,8) ||
     * +--------+-------+-------++-------+-------+-------+--------+-------+-------++
     * || (2,0) | (2,1) | (2,2) || (2,3) | (2,4) | (2,5) || (2,6) | (2,7) | (2,8) ||
     * +========+=======+=======+========+=======+=======+========+=======+=======++
     * || (3,0) | (3,1) | (3,2) || (3,3) | (3,4) | (3,5) || (3,6) | (3,7) | (3,8) ||
     * +--------+-------+-------++-------+-------+-------+--------+-------+-------++
     * || (4,0) | (4,1) | (4,2) || (4,3) | (4,4) | (4,5) || (4,6) | (4,7) | (4,8) ||
     * +--------+-------+-------++-------+-------+-------+--------+-------+-------++
     * || (5,0) | (5,1) | (5,2) || (5,3) | (5,4) | (5,5) || (5,6) | (5,7) | (5,8) ||
     * +========+=======+=======+========+=======+=======+========+=======+=======++
     */
    public class CellDataGenerator : IEnumerable<object[]> {
        private readonly List<object[]> _data = new List<object[]>
        {
            // 1
            new object[] {
                new Cell(0, 0), new List<Cell>(4) {
                    new Cell(0, 1),
                    new Cell(1, 0),
                    new Cell(3,5),
                    new Cell(3,2),
                }
            },
            new object[] {
                new Cell(0, 1), new List<Cell>(4) {
                    new Cell(0, 0),
                    new Cell(0, 2),
                    new Cell(1, 1),
                    new Cell(3, 4),
                },
            },
            new object[] {
                new Cell(0, 2), new List<Cell>(4) {
                    new Cell(0, 1),
                    new Cell(0, 3),
                    new Cell(1, 2),
                    new Cell(3, 3),
                },
            },
            new object[] {
                new Cell(1, 0), new List<Cell>(4) {
                    new Cell(0, 0),
                    new Cell(1,1),
                    new Cell(2, 0),
                    new Cell(4, 2),
                },
            },
            new object[] {
                new Cell(1, 1), new List<Cell>(4) {
                    new Cell(0, 1),
                    new Cell(1, 0),
                    new Cell(1, 2),
                    new Cell(2, 1),
                }
            },
            new object[] {
                new Cell(1, 2), new List<Cell>(4) {
                    new Cell(0, 2),
                    new Cell(1, 1),
                    new Cell(1, 3),
                    new Cell(2, 2),
                },
            },
            new object[] {
                new Cell(2, 0), new List<Cell>(4) {
                    new Cell(1, 0),
                    new Cell(2, 1),
                    new Cell(5, 2),
                    new Cell(3, 6),
                },
            },
            new object[] {
                new Cell(2, 1), new List<Cell>(4) {
                    new Cell(2, 0),
                    new Cell(1, 1),
                    new Cell(2, 2),
                    new Cell(3, 7),
                },
            },
            new object[] {
                new Cell(2, 2), new List<Cell>(4) {
                    new Cell(1, 2),
                    new Cell(2, 1),
                    new Cell(2, 3),
                    new Cell(3, 8),
                },
            },
            // 2
            new object[] {
                new Cell(0, 3), new List<Cell>(4) {
                    new Cell(0, 2),
                    new Cell(0, 4),
                    new Cell(1, 3),
                    new Cell(3, 3),
                },
            },
            new object[] {
                new Cell(0, 4), new List<Cell>(4) {
                    new Cell(0, 3),
                    new Cell(0, 5),
                    new Cell(1, 4),
                    new Cell(4, 3),
                },
            },
            new object[] {
                new Cell(0, 5), new List<Cell>(4) {
                    new Cell(0, 4),
                    new Cell(0, 6),
                    new Cell(1, 5),
                    new Cell(5, 3),
                },
            },
            new object[] {
                new Cell(1, 3), new List<Cell>(4) {
                    new Cell(0, 3),
                    new Cell(1, 2),
                    new Cell(1, 4),
                    new Cell(2, 3),
                },
            },
            new object[] {
                new Cell(1, 4), new List<Cell>(4) {
                    new Cell(0, 4),
                    new Cell(1, 3),
                    new Cell(1, 5),
                    new Cell(2, 4),
                },
            },
            new object[] {
                new Cell(1, 5), new List<Cell>(4) {
                    new Cell(0, 5),
                    new Cell(1, 4),
                    new Cell(1, 6),
                    new Cell(2, 5),
                },
            },
            new object[] {
                new Cell(2, 3), new List<Cell>(4) {
                    new Cell(1, 3),
                    new Cell(2, 2),
                    new Cell(2, 4),
                    new Cell(3, 8),
                },
            },
            new object[] {
                new Cell(2, 4), new List<Cell>(4) {
                    new Cell(1, 4),
                    new Cell(2, 3),
                    new Cell(2, 5),
                    new Cell(4, 8),
                },
            },
            new object[] {
                new Cell(2, 5), new List<Cell>(4) {
                    new Cell(1, 5),
                    new Cell(2, 4),
                    new Cell(2, 6),
                    new Cell(5, 8),
                },
            },
            // 3
            new object[] {
                new Cell(0, 6), new List<Cell>(4) {
                    new Cell(0, 5),
                    new Cell(0, 7),
                    new Cell(1, 6),
                    new Cell(5, 3),
                },
            },
            new object[] {
                new Cell(0, 7), new List<Cell>(4) {
                    new Cell(0, 6),
                    new Cell(0, 8),
                    new Cell(1, 7),
                    new Cell(5, 4),
                },
            },
            new object[] {
                new Cell(0, 8), new List<Cell>(4) {
                    new Cell(0, 7),
                    new Cell(3, 0),
                    new Cell(1, 8),
                    new Cell(5, 5),
                },
            },
            new object[] {
                new Cell(1, 6), new List<Cell>(4) {
                    new Cell(0, 6),
                    new Cell(1, 5),
                    new Cell(1, 7),
                    new Cell(2, 6),
                },
            },
            new object[] {
                new Cell(1, 7), new List<Cell>(4) {
                    new Cell(0, 7),
                    new Cell(1, 6),
                    new Cell(1, 8),
                    new Cell(2, 7),
                },
            },
            new object[] {
                new Cell(1, 8), new List<Cell>(4) {
                    new Cell(0, 8),
                    new Cell(1, 7),
                    new Cell(2, 8),
                    new Cell(4, 0),
                },
            },
            new object[] {
                new Cell(2, 6), new List<Cell>(4) {
                    new Cell(1, 6),
                    new Cell(2, 5),
                    new Cell(2, 7),
                    new Cell(5, 8),
                },
            },
            new object[] {
                new Cell(2, 7), new List<Cell>(4) {
                    new Cell(1, 7),
                    new Cell(2, 6),
                    new Cell(2, 8),
                    new Cell(5, 7),
                },
            },
            new object[] {
                new Cell(2, 8), new List<Cell>(4) {
                    new Cell(1, 8),
                    new Cell(2, 7),
                    new Cell(5, 0),
                    new Cell(5, 6),
                },
            },
            // 4
            new object[] {
                new Cell(3, 0), new List<Cell>(4) {
                    new Cell(3, 1),
                    new Cell(4, 0),
                    new Cell(0, 8),
                    new Cell(5, 5),
                },
            },
            new object[] {
                new Cell(3, 1), new List<Cell>(4) {
                    new Cell(3, 0),
                    new Cell(3, 2),
                    new Cell(4, 1),
                    new Cell(4, 5),
                },
            },
            new object[] {
                new Cell(3, 2), new List<Cell>(4) {
                    new Cell(3, 1),
                    new Cell(4, 2),
                    new Cell(0, 0),
                    new Cell(3, 5),
                },
            },
            new object[] {
                new Cell(4, 0), new List<Cell>(4) {
                    new Cell(3, 0),
                    new Cell(4, 1),
                    new Cell(5, 0),
                    new Cell(1, 8),
                },
            },
            new object[] {
                new Cell(4, 1), new List<Cell>(4) {
                    new Cell(3, 1),
                    new Cell(4, 0),
                    new Cell(4, 2),
                    new Cell(5, 1),
                },
            },
            new object[] {
                new Cell(4, 2), new List<Cell>(4) {
                    new Cell(3, 2),
                    new Cell(4, 1),
                    new Cell(5, 2),
                    new Cell(1, 0),
                },
            },
            new object[] {
                new Cell(5, 0), new List<Cell>(4) {
                    new Cell(4, 0),
                    new Cell(5, 1),
                    new Cell(2, 8),
                    new Cell(5, 6),
                },
            },
            new object[] {
                new Cell(5, 1), new List<Cell>(4) {
                    new Cell(4, 1),
                    new Cell(5, 0),
                    new Cell(5, 2),
                    new Cell(4, 6),
                },
            },
            new object[] {
                new Cell(5, 2), new List<Cell>(4) {
                    new Cell(4, 2),
                    new Cell(5, 1),
                    new Cell(2, 0),
                    new Cell(3, 6),
                },
            },
            // 5
            new object[] {
                new Cell(3, 3), new List<Cell>(4) {
                    new Cell(3, 4),
                    new Cell(4, 3),
                    new Cell(0, 2),
                    new Cell(0, 3),
                },
            },
            new object[] {
                new Cell(3, 4), new List<Cell>(4) {
                    new Cell(3, 3),
                    new Cell(3, 5),
                    new Cell(4, 4),
                    new Cell(0, 1),
                },
            },
            new object[] {
                new Cell(3, 5), new List<Cell>(4) {
                    new Cell(3, 4),
                    new Cell(4, 5),
                    new Cell(0, 0),
                    new Cell(3, 2),
                },
            },
            new object[] {
                new Cell(4, 3), new List<Cell>(4) {
                    new Cell(3, 3),
                    new Cell(4, 4),
                    new Cell(5, 3),
                    new Cell(0, 4),
                },
            },
            new object[] {
                new Cell(4, 4), new List<Cell>(4) {
                    new Cell(3, 4),
                    new Cell(4, 3),
                    new Cell(4, 5),
                    new Cell(5, 4),
                },
            },
            new object[] {
                new Cell(4, 5), new List<Cell>(4) {
                    new Cell(3, 5),
                    new Cell(4, 4),
                    new Cell(5, 5),
                    new Cell(3, 1),
                },
            },
            new object[] {
                new Cell(5, 3), new List<Cell>(4) {
                    new Cell(4, 3),
                    new Cell(5, 4),
                    new Cell(0, 5),
                    new Cell(0, 6),
                },
            },
            new object[] {
                new Cell(5, 4), new List<Cell>(4) {
                    new Cell(4, 4),
                    new Cell(5, 3),
                    new Cell(5, 5),
                    new Cell(0, 7),
                },
            },
            new object[] {
                new Cell(5, 5), new List<Cell>(4) {
                    new Cell(4, 5),
                    new Cell(5, 4),
                    new Cell(3, 0),
                    new Cell(0, 8),
                },
            },
            // 6
            new object[] {
                new Cell(3, 6), new List<Cell>(4) {
                    new Cell(3, 7),
                    new Cell(4, 6),
                    new Cell(2, 0),
                    new Cell(5, 2),
                },
            },
            new object[] {
                new Cell(3, 7), new List<Cell>(4) {
                    new Cell(3, 6),
                    new Cell(3, 8),
                    new Cell(4, 7),
                    new Cell(2, 1),
                },
            },
            new object[] {
                new Cell(3, 8), new List<Cell>(4) {
                    new Cell(3, 7),
                    new Cell(4, 8),
                    new Cell(2, 2),
                    new Cell(2, 3),
                },
            },
            new object[] {
                new Cell(4, 6), new List<Cell>(4) {
                    new Cell(3, 6),
                    new Cell(4, 7),
                    new Cell(5, 6),
                    new Cell(5, 1),
                },
            },
            new object[] {
                new Cell(4, 7), new List<Cell>(4) {
                    new Cell(3, 7),
                    new Cell(4, 6),
                    new Cell(4, 8),
                    new Cell(5, 7),
                },
            },
            new object[] {
                new Cell(4, 8), new List<Cell>(4) {
                    new Cell(3, 8),
                    new Cell(4, 7),
                    new Cell(5, 8),
                    new Cell(2, 4),
                },
            },
            new object[] {
                new Cell(5, 6), new List<Cell>(4) {
                    new Cell(4, 6),
                    new Cell(5, 7),
                    new Cell(2, 8),
                    new Cell(5, 0),
                },
            },
            new object[] {
                new Cell(5, 7), new List<Cell>(4) {
                    new Cell(4, 7),
                    new Cell(5, 6),
                    new Cell(5, 8),
                    new Cell(2, 7),
                },
            },
            new object[] {
                new Cell(5, 8), new List<Cell>(4) {
                    new Cell(4, 8),
                    new Cell(5, 7),
                    new Cell(2, 5),
                    new Cell(2, 6),
                },
            },
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class CubeGridTests {
        private const int SIDE_SIZE = 3;
        private readonly CubeGrid _grid = new CubeGrid(SIDE_SIZE, SIDE_SIZE);

        [Theory]
        [ClassData(typeof(CellDataGenerator))]
        public void NeighborsTest(Cell input, List<Cell> correct) {
            var assumed = _grid.Neighbors(input.X, input.Y);
            assumed.Sort(); correct.Sort();
            Assert.Equal(correct, assumed);
        }
    }
}
