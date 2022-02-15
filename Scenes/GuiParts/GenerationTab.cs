using System.Collections.Generic;
using Godot;
using GodotCSToolbox;
using Array = Godot.Collections.Array;

namespace MazeCube.Scenes.GuiParts {
    public class GenerationTab : VBoxContainer {

        // ReSharper disable FieldCanBeMadeReadOnly.Local
        [NodePath("OptionButton")] private OptionButton _optionButton = null;
        // ReSharper enable FieldCanBeMadeReadOnly.Local

        private readonly Dictionary<string, PackedScene> _scenes = new Dictionary<string, PackedScene> {
            ["Cylinder"] = GD.Load<PackedScene>("res://Scenes/GuiParts/CylinderSettings.tscn"),
            ["Cube"]     = GD.Load<PackedScene>("res://Scenes/GuiParts/CubeSettings.tscn"),
        };

        private ModelSettingsBase _guiNode;

        [Signal] public delegate void ClearMesh();
        [Signal] public delegate void SetupMesh(string name, Dictionary<string, object> settings);

        public override void _Ready() {
            this.SetupNodeTools();
        }

        public void OnOptionButtonItemSelected(int index) {
            var text = _optionButton.GetItemText(index);
            if (_scenes.ContainsKey(text)) {
                var child  = _scenes[text].Instance<ModelSettingsBase>();
                var button = child.GenerationButton;
                button.Connect(
                    signal: "button_up",
                    target: this,
                    method: nameof(GenerateButton),
                    binds: new Array() {text}
                );
                SetUpGuiChild(child);
                EmitSignal(nameof(SetupMesh), text, child.GetParams());
            } else {
                ClearGuiChild();
                EmitSignal(nameof(ClearMesh));
            }
        }

        private void ClearGuiChild() {
            _guiNode?.QueueFree();
            _guiNode = null;
        }

        private void SetUpGuiChild(ModelSettingsBase child) {
            ClearGuiChild();
            _guiNode = child;
            AddChild(child);
        }

        public void GenerateButton(string name) {
            EmitSignal(nameof(SetupMesh), name, _guiNode.GetParams());
        }

        private void Generate(string name, Dictionary<string, object> @params) {
            EmitSignal(nameof(SetupMesh), name, @params);
        }
    }
}
