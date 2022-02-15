using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class CylinderSettings : ModelSettingsBase {

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("CylinderOptionsGrid")] private Control _meshSettings = null;
        [NodePath("GenSideCheckBox")] private CheckBox _sideCheckBox = null;
        [NodePath("SideMazeGrid")] private GridContainer _sideParamContainer = null;
        [NodePath("GenTopCheckBox")] private CheckBox _topCheckBox = null;
        [NodePath("TopMazeGrid")] private GridContainer _topParamContainer = null;
        [NodePath("GenBottomCheckBox")] private CheckBox _bottomCheckBox = null;
        [NodePath("BottomMazeGrid")] private GridContainer _bottomParamContainer = null;
        // ReSharper enable FieldCanBeMadeReadOnly.Local

        public override void _Ready() {
            base._Ready();
            this.SetupNodeTools();

            _sideCheckBox.Pressed = true;

            _topCheckBox.Pressed  = false;
            _topCheckBox.Disabled = true;

            _bottomCheckBox.Pressed  = false;
            _bottomCheckBox.Disabled = true;

            _sideParamContainer.Visible   = _sideCheckBox.Pressed;
            _topParamContainer.Visible    = _topCheckBox.Pressed;
            _bottomParamContainer.Visible = _bottomCheckBox.Pressed;
        }

        private void UpdateVisibility(bool visible, Control control) {
            control.Visible = visible;
        }

        private void OnGenSideCheckBoxToggled(bool buttonPressed) =>
            UpdateVisibility(buttonPressed, _sideParamContainer);

        private void OnGenTopCheckBoxToggled(bool buttonPressed) =>
            UpdateVisibility(buttonPressed, _topParamContainer);

        private void OnGenBottomCheckBoxToggled(bool buttonPressed) =>
            UpdateVisibility(buttonPressed, _bottomParamContainer);

        private Dictionary<string, object> GetSideMazeParams() =>
            new Dictionary<string, object> {
                ["rows"]    = _sideParamContainer.GetNode<SpinBox>("GridVerticalSpinBox").Value,
                ["columns"] = _sideParamContainer.GetNode<SpinBox>("GridHorizontalSpinBox").Value,
                ["seed"]    = _sideParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
            };

        public override Dictionary<string, object> GetParams() =>
            new Dictionary<string, object> {
                ["mesh_options"] = _meshSettings.Call("get_params"),
                ["side_maze"]    = GetSideMazeParams(),
                ["top_maze"]     = new Dictionary<string, object>(),
                ["bottom_maze"]  = new Dictionary<string, object>(),
            };
    }
}
