using System;
using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class MobiusSettings : ModelSettingsBase {

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("MazeGrid")] private GridContainer _mazeParamContainer = null;
        // ReSharper enable FieldCanBeMadeReadOnly.Local

        public override void _Ready() {
            base._Ready();
            this.SetupNodeTools();
        }

        public override Dictionary<string, object> GetParams()
            => new() {
                ["maze"] = new Dictionary<string, object> {
                    ["rows"] = _mazeParamContainer.GetNode<SpinBox>("GridVerticalSpinBox").Value,
                    ["cols"] = _mazeParamContainer.GetNode<SpinBox>("GridHorizontalSpinBox").Value,
                    ["seed"] = _mazeParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
                }
            };
    }
}
