using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Godot;
using MazeCube.Scripts.MazeGen.Grid;
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

        // ReSharper disable once UnusedMember.Global
        // Reason: Called from signal
        public void ChangeMesh(string name, IDictionary<string, IDictionary<string, object>> settings) {
            switch (name) {
                case "Cylinder":
                    this.Mesh       = new CylinderMesh();
                    _texturePainter = new CylinderPainter(500);
                    break;
                case "Cube":
                    this.Mesh       = GD.Load<Godot.Mesh>("res://Assets/Models/cube.obj");
                    _texturePainter = new CubePainter(600);
                    break;
                case "Mobius":
                    this.Mesh       = GD.Load<Godot.Mesh>("res://Assets/Models/mobius.obj");
                    _texturePainter = new MobiusPainter(500);
                    break;
                default:
                    ClearMesh();
                    return;
            }

            ConfigureMesh(name, settings);
        }

        private void ConfigureMesh(string name, IDictionary<string, IDictionary<string, object>> settings) {
            var properties = new Dictionary<string, Dictionary<string, object>>();
            if (settings != null) {
                foreach (var section in settings.Keys) {
                    properties[section] = new Dictionary<string, object>();
                    foreach (var key in settings[section].Keys) {
                        properties[section][key] = settings[section][key];
                    }
                }
            }

            if (_material == null) {
                _material = new SpatialMaterial();
                this.SetSurfaceMaterial(0, _material);
            }

            switch (name) {
                case "Cylinder":
                    var cylinder = (CylinderMesh)this.Mesh;
                    cylinder.Height         = Convert.ToSingle(properties["mesh_options"]["height"]);
                    cylinder.Rings          = Convert.ToInt32(properties["mesh_options"]["rings"]);
                    cylinder.BottomRadius   = Convert.ToSingle(properties["mesh_options"]["bottom_radius"]);
                    cylinder.TopRadius      = Convert.ToSingle(properties["mesh_options"]["top_radius"]);
                    cylinder.RadialSegments = Convert.ToInt32(properties["mesh_options"]["radial_segments"]);

                    CylinderGrid cylinderGrid;
                    if (properties["side_maze"].Count > 0) {
                        var rows = Convert.ToInt32(properties["side_maze"]["rows"]);
                        var cols = Convert.ToInt32(properties["side_maze"]["columns"]);
                        var seed = Convert.ToString(properties["side_maze"]["seed"]).Trim();

                        cylinderGrid = new CylinderGrid(rows, cols);

                        ApplyMazeGenAlgo<RecursiveBacktracker>(cylinderGrid, seed);
                    } else {
                        cylinderGrid = new CylinderGrid(0, 0);
                    }

                    if (properties["top_maze"].Count > 0) {
                        var ringsTop = Convert.ToInt32(properties["top_maze"]["rings"]);
                        var cellsTop = Convert.ToInt32(properties["top_maze"]["cells"]);
                        var seedTop  = Convert.ToString(properties["top_maze"]["seed"]).Trim();

                        cylinderGrid.AddTopGrid(ringsTop, cellsTop);
                        ApplyMazeGenAlgo<RecursiveBacktracker>(cylinderGrid.TopGrid, seedTop);
                    }

                    if (properties["bottom_maze"].Count > 0) {
                        var ringsBottom = Convert.ToInt32(properties["bottom_maze"]["rings"]);
                        var cellsBottom = Convert.ToInt32(properties["bottom_maze"]["cells"]);
                        var seedBottom  = Convert.ToString(properties["bottom_maze"]["seed"]).Trim();

                        cylinderGrid.AddBottomGrid(ringsBottom, cellsBottom);
                        ApplyMazeGenAlgo<RecursiveBacktracker>(cylinderGrid.BottomGrid, seedBottom);
                    }

                    cylinderGrid.DrawInConsole();
                    _material.AlbedoTexture = _texturePainter.CreateImageTexture(cylinderGrid);

                    break;
                case "Cube":
                    var cubeGrid = new CubeGrid(3, 3);
                    ApplyMazeGenAlgo<RecursiveBacktracker>(cubeGrid, "666");

                    _material.AlbedoTexture = _texturePainter.CreateImageTexture(cubeGrid);

                    break;
                case "Mobius":
                    var mobiusRows    = Convert.ToInt32(properties["maze"]["rows"]);
                    var mobiusCols    = Convert.ToInt32(properties["maze"]["cols"]);
                    var mobiusRawSeed = Convert.ToString(properties["maze"]["seed"]);
                    var mobiusGrid    = new MobiusGrid(mobiusRows, mobiusCols);
                    ApplyMazeGenAlgo<RecursiveBacktracker>(mobiusGrid, mobiusRawSeed);

                    _material.AlbedoTexture = _texturePainter.CreateImageTexture(mobiusGrid);

                    break;
                default:
                    throw new NotSupportedException($"Invalid name: {name}");
            }
        }

        private Dictionary<string, Dictionary<string, object>> GetDefaultSettings(string name) {
            try {
                using var stream = s_assembly.GetManifestResourceStream(PREFIX + $"{name.Capitalize()}.json");
                Debug.Assert(stream != null, nameof(stream) + " != null");
                using var textReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(textReader);
                return JToken.ReadFrom(jsonReader)
                    .ToObject<Dictionary<string, Dictionary<string, object>>>();
            } catch (Exception ex) {
                Log.Logger.Error(ex, "Failed to parse default parameters for {Name}", name);
                return new Dictionary<string, Dictionary<string, object>>();
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // Reason: Called from signal
        public void ClearMesh() {
            this.Mesh = null;
        }

        private void ApplyMazeGenAlgo<T>(Grid.Grid grid, string rawSeed) where T : RandomMaze {
            RandomMaze mazeGen;
            if (string.IsNullOrWhiteSpace(rawSeed)) {
                mazeGen = (RandomMaze)Activator.CreateInstance(typeof(T), new object[] {null});
            } else {
                if (!int.TryParse(rawSeed, out var realSeed)) {
                    realSeed = rawSeed.GetHashCode();
                }

                mazeGen = (RandomMaze)Activator.CreateInstance(typeof(T), (int?)realSeed);
            }

            mazeGen.Project(grid);
        }
    }
}
