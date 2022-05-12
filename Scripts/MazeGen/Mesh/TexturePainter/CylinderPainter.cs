using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Godot;
using MazeCube.Scripts.MazeGen.Grid;
using Color = System.Drawing.Color;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public class CylinderPainter : TexturePainter {
        public CylinderPainter(int side) : base(side) {
        }

        protected override byte[] CreatePngImage(Grid.Grid grid) {
            if (grid is not CylinderGrid cylinderGrid) {
                throw new ArgumentException("Must be of CylinderGrid", nameof(grid));
            }

            using var image = new Bitmap(Side, Side);
            PaintTexture(image, cylinderGrid);

            using var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

        private void PaintTexture(System.Drawing.Image image, CylinderGrid grid) {
            var allImage      = new Rectangle(0, 0, Side, Side);
            var sideRectangle = new Rectangle(0, 0, Side, Side / 2);
            var topCircle     = new Rectangle(0, Side / 2, Side / 2, Side / 2);
            var bottomCircle  = new Rectangle(Side / 2, Side / 2, Side / 2, Side / 2);

            using var g = Graphics.FromImage(image);
            using (var errorBrush = new SolidBrush(ErrorColor)) {
                g.FillRectangle(errorBrush, allImage);
            }

            g.PaintPolarMaze(topCircle, grid.TopGrid, FgColor, BgColor);
            g.PaintPolarMaze(bottomCircle, grid.BottomGrid, FgColor, BgColor);
            g.PaintRectMaze(sideRectangle, grid, FgColor, BgColor);
        }
    }
}
