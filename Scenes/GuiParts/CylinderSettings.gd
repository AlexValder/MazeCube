extends VBoxContainer

onready var _meshSettings := $CylinderOptionsGrid as CylinderOptions
onready var _sideCheckBox := $GenSideCheckBox as CheckBox
onready var _sideParamContainer := $SideMazeGrid as GridContainer
onready var _topCheckBox := $GenTopCheckBox as CheckBox
onready var _topParamContainer := $TopMazeGrid as GridContainer
onready var _bottomCheckBox := $GenBottonCheckBox as CheckBox
onready var _bottomParamContainer := $BottomMazeGrid as GridContainer


func _ready() -> void:
    # Defaults
    _sideCheckBox.pressed = true

    _topCheckBox.pressed = false
    _topCheckBox.disabled = true

    _bottomCheckBox.pressed = false
    _bottomCheckBox.disabled = true

    _sideParamContainer.visible = _sideCheckBox.pressed
    _topParamContainer.visible = _topCheckBox.pressed
    _bottomParamContainer.visible = _bottomCheckBox.pressed


func _updateVisibility(visible: bool, control: Control) -> void:
    control.visible = visible


func _on_GenSideCheckBox_toggled(button_pressed: bool) -> void:
    _updateVisibility(button_pressed, _sideParamContainer)


func _on_GenTopCheckBox_toggled(button_pressed: bool) -> void:
    _updateVisibility(button_pressed, _topParamContainer)


func _on_GenBottonCheckBox_toggled(button_pressed: bool) -> void:
    _updateVisibility(button_pressed, _bottomParamContainer)


func _get_side_maze_params() -> Dictionary:
    return {
        "rows": _sideParamContainer.get_node("GridVerticalSpinBox").value,
        "columns": _sideParamContainer.get_node("GridHorizontalSpinBox").value,
        "seed": _sideParamContainer.get_node("SeedLineEdit").text,
       }


func get_params() -> Dictionary:
    var params := {}
    params["mesh_options"] = _meshSettings.get_params()
    params["side_maze"] = _get_side_maze_params()
    params["top_maze"] = {}
    params["bottom_maze"] = {}
    return params
