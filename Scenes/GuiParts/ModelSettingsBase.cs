using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public abstract class ModelSettingsBase : Control {
        public Button GenerationButton { get; }

        protected ModelSettingsBase() {
            GenerationButton = PrepareButton();
        }

        public override void _Ready() {
            this.AddChild(GenerationButton);
        }

        private Button PrepareButton() => new() {
            Text = "Generate",
            Name = "GenerationButton",
        };

        public abstract Dictionary<string, object> GetParams();
    }
}
