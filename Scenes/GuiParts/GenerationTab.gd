extends VBoxContainer
class_name GenerationTab

onready var _optionButton := $OptionButton as OptionButton
onready var _scenes := {
    "Cylinder" : preload("res://Scenes/GuiParts/CylinderSettings.tscn"),
   }
onready var _guiNode: Control = null

signal clear_mesh
signal setup_mesh(name, settings)


func _on_OptionButton_item_selected(index: int) -> void:
    var text := _optionButton.get_item_text(index)
    if text in _scenes.keys():
        var child = _scenes[text].instance()
        var button := child.get_node("GenerateButton") as Button
        button.connect("button_up", self, "generate_button", [text])
        setUpGuiChild(child)
        emit_signal("setup_mesh", text, child.get_params())
    else:
        clearGuiChild()
        emit_signal("clear_mesh")


func clearGuiChild() -> void:
    if _guiNode == null:
        return
    _guiNode.queue_free()
    _guiNode = null


func setUpGuiChild(child: Control) -> void:
    clearGuiChild()
    _guiNode = child
    add_child(child)


func generate_button(name: String) -> void:
    emit_signal("setup_mesh", name, _guiNode.get_params())


func generate(name: String, params: Dictionary) -> void:
    emit_signal("setup_mesh", name, params)
