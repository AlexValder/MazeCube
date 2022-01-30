using System.Collections.Generic;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class Cell {
        public int Row { get; }
        public int Column { get; }
        public Cell North { get; set; }
        public Cell East { get; set; }
        public Cell West { get; set; }
        public Cell South { get; set; }

        private readonly Dictionary<Cell, bool> _links = new Dictionary<Cell, bool>();

        public IEnumerable<Cell> Links => _links.Keys;

        private Directions? _directions = null;
        public Directions Directions {
            get {
                if (_directions != null) {
                    return _directions.Value;
                }
                switch (_links.Keys.Count) {
                    case 0:
                        return Directions.None;
                    case 1:
                        if (IsLinked(North)) {
                            return Directions.North;
                        } else if (IsLinked(East)) {
                            return Directions.East;
                        } else if (IsLinked(South)) {
                            return Directions.South;
                        } else {
                            return Directions.West;
                        }
                    case 2:
                        if (IsLinked(North) && IsLinked(East)) {
                            return Directions.North | Directions.East;
                        } else if (IsLinked(North) && IsLinked(South)) {
                            return Directions.North | Directions.South;
                        } else if (IsLinked(North) && IsLinked(West)) {
                            return Directions.North | Directions.West;
                        } else if (IsLinked(East) && IsLinked(South)) {
                            return Directions.East | Directions.South;
                        } else if (IsLinked(West) && IsLinked(South)) {
                            return Directions.West | Directions.South;
                        } else {
                            return Directions.West | Directions.East;
                        }
                    case 3:
                        if (!IsLinked(North)) {
                            return Directions.East | Directions.South | Directions.West;
                        } else if (!IsLinked(East)) {
                            return Directions.North | Directions.South | Directions.West;
                        } else if (!IsLinked(South)) {
                            return Directions.North | Directions.East | Directions.West;
                        } else {
                            return Directions.North | Directions.East | Directions.South;
                        }
                    case 4:
                        return Directions.North | Directions.East | Directions.South | Directions.West;
                    default:
                        return Directions.None;
                }
            }
        }

        public IEnumerable<Cell> Neighbors {
            get {
                var list = new List<Cell>(4);
                if (North != null) list.Add(North);
                if (East != null) list.Add(East);
                if (West != null) list.Add(West);
                if (South != null) list.Add(South);
                return list;
            }
        }

        public Cell(int row, int column) {
            Row    = row;
            Column = column;
        }

        public Cell Link(Cell cell, bool bidirectionally = true) {
            _links[cell] = true;
            if (bidirectionally) {
                cell.Link(this, false);
            }

            return this;
        }

        public Cell Unlink(Cell cell, bool bidirectionally = true) {
            _links.Remove(cell);
            if (bidirectionally) {
                cell.Unlink(this, false);
            }

            return this;
        }

        public bool IsLinked(Cell cell) {
            return cell != null && _links.ContainsKey(cell);
        }
    }
}
