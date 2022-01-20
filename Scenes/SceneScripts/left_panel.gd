extends VBoxContainer

onready var _panel := $panel as Control
onready var _hide_button := $hbox/HideShowGui as Button

var _panel_hidden := false

const _INVISIBLE_COLOR := Color(1.0, 1.0, 1.0, 0.0)
const _VISIBLE_COLOR := Color(1.0, 1.0, 1.0, 1.0)

func _ready() -> void:
    OS.window_maximized = true


func _on_HideShowGui_button_up() -> void:
    if _panel_hidden:
        _panel_hidden = false
        _panel.modulate = _VISIBLE_COLOR
        _hide_button.text = "Hide"
    else:
        _panel_hidden = true
        _panel.modulate = _INVISIBLE_COLOR
        _hide_button.text = "Show"


func _on_FullscreenCheckButton_toggled(button_pressed: bool) -> void:
    OS.window_fullscreen = button_pressed


func _on_ExitButton_button_up() -> void:
    get_tree().quit(0)
