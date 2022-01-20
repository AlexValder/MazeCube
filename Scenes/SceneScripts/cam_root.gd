extends Spatial

onready var _v := $h/v as Spatial
onready var _h := $h as Spatial
onready var _camera := $h/v/Camera as Camera

var mouse_sensitivity := 120.0
const MOUSE_WHEEL_STEP := 0.5
const DEFAULT_DISTANCE := 8
const MIN_DISTANCE := 5
const MAX_DISTANCE := 30

func _ready():
    _reset_rotation()
    _reset_zoom()


func _input(event: InputEvent) -> void:
    if event.is_action_released("camera_center"):
        _reset_rotation()
    elif event.is_action_released("camera_zoom_in"):
        _zoom_in()
    elif event.is_action_released("camera_zoom_out"):
        _zoom_out()
    elif event is InputEventMouseMotion:
        if Input.is_mouse_button_pressed(BUTTON_MASK_LEFT):
            _rotate_horizontal(-event.relative.x / mouse_sensitivity)
            _rotate_vertical(-event.relative.y / mouse_sensitivity)


func _reset_zoom() -> void:
    _camera.transform.origin.z = DEFAULT_DISTANCE


func _reset_rotation() -> void:
    _h.rotation_degrees = Vector3.ZERO
    _v.rotation_degrees = Vector3.ZERO


func _rotate_horizontal(delta: float) -> void:
    _h.rotate_y(delta)


func _zoom_in() -> void:
    if _camera.transform.origin.z - MOUSE_WHEEL_STEP <= MIN_DISTANCE:
        _camera.transform.origin.z = MIN_DISTANCE
    else:
        _camera.transform.origin.z -= MOUSE_WHEEL_STEP


func _zoom_out() -> void:
    if _camera.transform.origin.z + MOUSE_WHEEL_STEP >= MAX_DISTANCE:
        _camera.transform.origin.z = MAX_DISTANCE
    else:
        _camera.transform.origin.z += MOUSE_WHEEL_STEP


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
