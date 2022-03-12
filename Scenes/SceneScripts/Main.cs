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

            ConnectOrQuit(_meshChange, "ClearMesh", _meshRoot, "ClearMesh");
            ConnectOrQuit(_meshChange, "SetupMesh", _meshRoot, "ChangeMesh");
            ConnectOrQuit(_panel, "mouse_entered", _camera, "disable_camera");
            ConnectOrQuit(_panel, "mouse_exited", _camera, "enable_camera");
        }

        public override void _Input(InputEvent @event) {
            if (@event.IsActionReleased("quit")) {
                Quit(0);
            }
        }

        private void ConnectOrQuit(Node from, string signal, Node to, string method) {
            var error = from.Connect(signal, to, method);
            if (error != Error.Ok) {
                Log.Error(
                    "Failed to connect {From}.{Signal} to {To}.{Method}",
                    from.Name, signal, to.Name, method
                );
                Quit(-1);
            }
        }

        private void Quit(int code) {
            GetTree().Quit(code);
        }
    }
}
