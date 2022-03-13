using System;
using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class CylinderOptionsGrid : GridContainer {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("HeightSlider")] private Slider _heightSlider = null;
        [NodePath("TopRadiusSlider")] private Slider _topRadiusSlider = null;
        [NodePath("BottomRadiusSlider")] private Slider _bottomRadiusSlider = null;
        [NodePath("RadialSegmentsSlider")] private Slider _radialSegmentsSlider = null;
        [NodePath("RingsSlider")] private Slider _ringsSlider = null;
        // ReSharper enable FieldCanBeMadeReadOnly.Local

        private Dictionary<string, SliderData> _params;

        private struct SliderData {
            public Slider Slider;
            public Label ValueLabel;
            public bool IsInt;
        }

        public override void _Ready() {
            this.SetupNodeTools();

            _params = GetSliderData();

            foreach (var value in _params.Values) {
                UpdateNumber(value.Slider.Value, value.ValueLabel, value.IsInt);
                var error = value.Slider.Connect(
                    "value_changed",
                    this,
                    nameof(UpdateNumber),
                    new Godot.Collections.Array {value.ValueLabel, value.IsInt}
                );
                if (error != Error.Ok) {
                    GD.PushError($"Failed to connect: {error}");
                }
            }
        }

        public Dictionary<string, object> GetParams() {
            var result = new Dictionary<string, object>(_params.Count);
            foreach (var pair in _params) {
                result[pair.Key] = pair.Value.Slider.Value;
            }
            return result;
        }

        private void UpdateNumber(double value, Label label, bool convertToInt) {
            label.Text = convertToInt ? $"{Convert.ToInt32(value)}" : $"{value:F2}";
        }

        private Dictionary<string, SliderData> GetSliderData() => new Dictionary<string, SliderData> {
            ["height"] = new() {
                Slider     = _heightSlider,
                ValueLabel = GetNode<Label>("valueLabel1"),
                IsInt      = false,
            },
            ["top_radius"] = new() {
                Slider     = _topRadiusSlider,
                ValueLabel = GetNode<Label>("valueLabel2"),
                IsInt      = false,
            },
            ["bottom_radius"] = new() {
                Slider     = _bottomRadiusSlider,
                ValueLabel = GetNode<Label>("valueLabel3"),
                IsInt      = false,
            },
            ["radial_segments"] = new() {
                Slider     = _radialSegmentsSlider,
                ValueLabel = GetNode<Label>("valueLabel4"),
                IsInt      = true,
            },
            ["rings"] = new() {
                Slider     = _ringsSlider,
                ValueLabel = GetNode<Label>("valueLabel5"),
                IsInt      = true,
            },
        };
    }
}
