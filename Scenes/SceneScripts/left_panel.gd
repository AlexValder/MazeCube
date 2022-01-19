extends VBoxContainer

onready var _panel := $panel as Control
onready var _hide_button := $hbox/HideShowGui as Button

var _panel_hidden := false


func _on_HideShowGui_button_up() -> void:
    if _panel_hidden:
        _panel_hidden = false
        _panel.modulate.a = 1.0
        _hide_button.text = "Hide"
    else:
        _panel_hidden = true
        _panel.modulate.a = 0.0
        _hide_button.text = "Show"
