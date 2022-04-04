using System;
using System.Collections.Generic;
using Godot;

namespace MazeCube.Scripts.MazeGen.Grid {
    public class CylinderGrid : CircularGrid {
        public PolarGrid TopGrid { get; private set; }
        public PolarGrid BottomGrid { get; private set; }

        public CylinderGrid(int rows, int columns) : base(rows, columns) { }

        public void AddTopGrid(int rings, int cells) {
            if (TopGrid != null) return;

            TopGrid = new PolarGrid(rings, cells);
        }

        public void AddBottomGrid(int rings, int cells) {
            if (BottomGrid != null) return;

            BottomGrid = new PolarGrid(rings, cells);
        }

        public override void DrawInConsole() {
            GD.Print("=== SIDE ===");
            DrawInConsole(this);
            if (TopGrid != null) {
                GD.Print("=== TOP ===");
                DrawInConsole(TopGrid);
            }

            if (BottomGrid != null) {
                GD.Print("=== BOTTOM ===");
                DrawInConsole(BottomGrid);
            }
        }
    }
}
