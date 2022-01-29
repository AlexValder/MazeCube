extends Node

onready var _gui := $GUI as Control
onready var _meshChange := $GUI/LeftPanel/panel/Generation/
onready var _camera := $CamRoot as Spatial
onready var _meshRoot := $MeshRoot as MeshInstance


func _ready() -> void:
    var error: int
    error = _meshChange.connect("clear_mesh", _meshRoot, "ClearMesh")
    if error != OK:
        push_error("Failed to connect \"clear_mesh\" to \"ClearMesh\"")
        get_tree().quit(-1)

    error = _meshChange.connect("setup_mesh", _meshRoot, "ChangeMesh")
    if error != OK:
        push_error("Failed to connect \"setup_mesh\" to \"ChangeMesh\"")
        get_tree().quit(-1)
