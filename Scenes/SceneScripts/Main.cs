using System;
using Godot;
using GodotCSToolbox;
using Serilog;

namespace MazeCube.Scenes.SceneScripts {
    public class Main : Node {
        private static readonly string s_logPath = AppDomain.CurrentDomain.BaseDirectory + "/log/log.log";

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("MeshRoot")]
        private MeshInstance _meshRoot = null;
        [NodePath("GUI/LeftPanel/")]
        private Control _panel = null;
        [NodePath("CamRoot")]
        private Spatial _camera = null;
        [NodePath("GUI/LeftPanel/panel/Generation/")]
        private Control _meshChange = null;
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        static Main() {
            SetupLogger();
        }

        private static void SetupLogger() {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .WriteTo.File(s_logPath)
                .MinimumLevel.Error()
                .CreateLogger();
        }

        public override void _Ready() {
            this.SetupNodeTools();

            var error = _meshChange.Connect("clear_mesh", _meshRoot, "ClearMesh");
            if (error != Error.Ok) {
                Log.Error("Failed to connect \"clear_mesh\" to \"ClearMesh\"");
                Quit(-1);
            }

            error = _meshChange.Connect("setup_mesh", _meshRoot, "ChangeMesh");
            if (error != Error.Ok) {
                Log.Error("Failed to connect \"setup_mesh\" to \"ChangeMesh\"");
                Quit(-1);
            }

            error = _panel.Connect("mouse_entered", _camera, "disable_camera");
            if (error != Error.Ok) {
                Log.Error("Failed to connect \"mouse_entered\" to \"disable_camera\"");
                Quit(-1);
            }

            error = _panel.Connect("mouse_exited", _camera, "enable_camera");
            if (error != Error.Ok) {
                Log.Error("Failed to connect \"mouse_exited\" to \"enable_camera\"");
                Quit(-1);
            }
        }

        private void Quit(int code = 0) {
            GetTree().Quit(code);
        }
    }
}
