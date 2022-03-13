using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class CylinderSettings : ModelSettingsBase {

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("CylinderOptionsGrid")] private CylinderOptionsGrid _meshSettings = null;
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

            _sideCheckBox.Pressed   = true;
            _topCheckBox.Pressed    = true;
            _bottomCheckBox.Pressed = true;

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
            _sideCheckBox.Pressed
                ? new Dictionary<string, object> {
                    ["rows"]    = _sideParamContainer.GetNode<SpinBox>("GridVerticalSpinBox").Value,
                    ["columns"] = _sideParamContainer.GetNode<SpinBox>("GridHorizontalSpinBox").Value,
                    ["seed"]    = _sideParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
                }
                : new Dictionary<string, object>();

        private Dictionary<string, object> GetTopMazeParams() =>
            _topCheckBox.Pressed
                ? new Dictionary<string, object> {
                    ["rings"] = _topParamContainer.GetNode<SpinBox>("GridTopInRadiusSpinBox").Value,
                    ["cells"] = _topParamContainer.GetNode<SpinBox>("GridTopRingsSpinBox").Value,
                    ["seed"]  = _topParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
                }
                : new Dictionary<string, object>();

        private Dictionary<string, object> GetBottomMazeParams() =>
            _bottomCheckBox.Pressed
                ? new Dictionary<string, object> {
                    ["rings"] = _bottomParamContainer.GetNode<SpinBox>("GridBottomInRadiusSpinBox").Value,
                    ["cells"] = _bottomParamContainer.GetNode<SpinBox>("GridBottomRingsSpinBox").Value,
                    ["seed"]  = _bottomParamContainer.GetNode<LineEdit>("SeedLineEdit").Text,
                }
                : new Dictionary<string, object>();

        public override Dictionary<string, object> GetParams() =>
            new() {
                ["mesh_options"] = _meshSettings.GetParams(),
                ["side_maze"]    = GetSideMazeParams(),
                ["top_maze"]     = GetTopMazeParams(),
                ["bottom_maze"]  = GetBottomMazeParams(),
            };
    }
}
