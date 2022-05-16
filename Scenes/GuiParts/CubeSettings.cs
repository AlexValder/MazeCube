using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class CubeSettings : ModelSettingsBase {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("MazeGrid")] private GridContainer _gridParamContainer = null;
        // ReSharped enable FieldCanBeMadeReadOnly.Local

        public override void _Ready() {
            base._Ready();
            this.SetupNodeTools();
        }

        public override Dictionary<string, object> GetParams() {
            return new() {
                ["maze"] = new Dictionary<string, object> {
                    ["side"] = _gridParamContainer.GetNode<SpinBox>("GridSideSizeSpinBox").Value,
                    ["seed"] = _gridParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
                },
            };
        }
    }
}
