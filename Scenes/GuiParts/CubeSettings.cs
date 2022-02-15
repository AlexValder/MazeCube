using System.Collections.Generic;
using Godot;
using GodotCSToolbox;

namespace MazeCube.Scenes.GuiParts {
    public class CubeSettings : ModelSettingsBase {
        public override void _Ready() {
            base._Ready();
            this.SetupNodeTools();
        }

        public override Dictionary<string, object> GetParams() {
            return new Dictionary<string, object>();
        }
    }
}
