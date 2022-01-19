extends Spatial

onready var _v := $h/v as Spatial
onready var _h := $h as Spatial
onready var _camera := $h/v/Camera as Camera

var mouse_sensitivity := 120.0

func _ready():
    pass


func _input(event: InputEvent) -> void:
    if event.is_action_released("camera_center"):
        _reset_rotation()
    elif event is InputEventMouseMotion:
        if Input.is_mouse_button_pressed(BUTTON_MASK_LEFT):
            _rotate_horizontal(-event.relative.x / mouse_sensitivity)
            _rotate_vertical(-event.relative.y / mouse_sensitivity)


func _reset_rotation() -> void:
    _h.rotation_degrees = Vector3.ZERO
    _v.rotation_degrees = Vector3.ZERO


func _rotate_horizontal(delta: float) -> void:
    _h.rotate_y(delta)


func _rotate_vertical(delta: float) -> void:
    if delta > 0:
        if _v.rotation_degrees.x + delta > 90:
            _v.rotation_degrees.x = 90
        else:
            _v.rotate_x(delta)
    else:
        if _v.rotation_degrees.x + delta < -90:
            _v.rotation_degrees.x = -90
        else:
            _v.rotate_x(delta)
