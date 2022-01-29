using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Godot;
using MazeCube.Scripts.MazeGen.MazeAlgo;
using MazeCube.Scripts.MazeGen.Mesh.TexturePainter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace MazeCube.Scripts.MazeGen.Mesh {
    public class MeshDisplay : MeshInstance {
        private const string PREFIX = "MazeCube.DefaultMeshProperties.";
        private static readonly Assembly s_assembly = Assembly.GetExecutingAssembly();
        private SpatialMaterial _material;
        private TexturePainter.TexturePainter _texturePainter;

        public override void _Ready() {
            _material = GetActiveMaterial(0) as SpatialMaterial;
            ClearMesh();
        }

        public void ChangeMesh(string name, IDictionary<string, IDictionary<string, object>> settings) {
            switch (name) {
                case "Cylinder":
                    this.Mesh       = new CylinderMesh();
                    _texturePainter = new CylinderPainter(500);
                    break;
                default:
                    ClearMesh();
                    return;
            }

            ConfigureMesh(name, settings);
        }

        private void ConfigureMesh(string name, IDictionary<string, IDictionary<string, object>> settings) {
            var properties = GetDefaultSettings(name);
            if (settings != null) {
                foreach (var section in settings.Keys) {
                    foreach (var key in settings[section].Keys) {
                        properties[section][key] = settings[section][key];
                    }
                }
            }

            switch (name) {
                case "Cylinder":
                    var cylinder = (CylinderMesh)this.Mesh;
                    cylinder.Height         = Convert.ToSingle(properties["mesh_options"]["height"]);
                    cylinder.Rings          = Convert.ToInt32(properties["mesh_options"]["rings"]);
                    cylinder.BottomRadius   = Convert.ToSingle(properties["mesh_options"]["bottom_radius"]);
                    cylinder.TopRadius      = Convert.ToSingle(properties["mesh_options"]["top_radius"]);
                    cylinder.RadialSegments = Convert.ToInt32(properties["mesh_options"]["radial_segments"]);

                    var rows    = Convert.ToInt32(properties["side_maze"]["rows"]);
                    var cols    = Convert.ToInt32(properties["side_maze"]["columns"]);
                    var seed = Convert.ToString(properties["side_maze"]["seed"]).Trim();

                    var grid = new Grid.Grid(rows, cols);

                    if (string.IsNullOrWhiteSpace(seed)) {
                        new RecursiveBacktracker().Project(grid);
                    } else {
                        if (!int.TryParse(seed, out var realSeed)) {
                            realSeed = seed.GetHashCode();
                        }
                        new RecursiveBacktracker(realSeed).Project(grid);
                    }

                    grid.DrawInConsole();
                    _material.AlbedoTexture = _texturePainter.CreateImageTexture(grid);

                    break;
            }
        }

        private Dictionary<string, Dictionary<string, object>> GetDefaultSettings(string name) {
            try {
                using (var stream = s_assembly.GetManifestResourceStream(PREFIX + $"{name.Capitalize()}.json")) {
                    Debug.Assert(stream != null, nameof(stream) + " != null");
                    using (var textReader = new StreamReader(stream))
                    using (var jsonReader = new JsonTextReader(textReader)) {
                        return JToken.ReadFrom(jsonReader)
                            .ToObject<Dictionary<string, Dictionary<string, object>>>();
                    }
                }
            } catch (Exception ex) {
                Log.Logger.Error(ex, "Failed to parse default parameters for {Name}", name);
                return null;
            }
        }

        public void ClearMesh() {
            this.Mesh = null;
        }
    }
}
