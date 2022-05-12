using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using Godot;
using Directory = System.IO.Directory;
using Path = System.IO.Path;
using File = System.IO.File;
using Image = System.Drawing.Image;
using Color = System.Drawing.Color;

namespace MazeCube.Scripts.MazeGen.Mesh.TexturePainter {
    public abstract class TexturePainter {
        public int Side { get; }
        public Color ErrorColor { get; set; } = Color.Red;
        public Color BgColor { get; set; } = Color.Black;
        public Color FgColor { get; set; } = Color.White;

        protected TexturePainter(int side) {
            Side = side;
        }

        protected abstract byte[] CreatePngImage(Grid.Grid grid);
        public Godot.Image CreateImage(Grid.Grid grid) {
            var image = new Godot.Image();
            image.LoadPngFromBuffer(CreatePngImage(grid));
            return image;
        }
        public ImageTexture CreateImageTexture(Grid.Grid grid) {
            var texture = new ImageTexture();
            texture.CreateFromImage(CreateImage(grid));
            return texture;
        }

        [Conditional("DEBUG")]
        protected static void Save(Image image, string output) {
            try {
                Directory.CreateDirectory(Path.GetDirectoryName(output)!);
                using var stream = File.Create(output);
                image.Save(stream, ImageFormat.Png);
            } catch (Exception ex) {
                Console.WriteLine($"FAILED to save image. {ex}: {ex.Message}");
            }
        }
    }
}
