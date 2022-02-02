extends GridContainer
class_name CylinderOptions

onready var _heightSlider := $HeightSlider as Slider
onready var _topRadiusSlider := $TopRadiusSlider as Slider
onready var _bottomRadiusSlider := $BottomRadiusSlider as Slider
onready var _radialSegmentsSlider := $RadialSegmentsSlider as Slider
onready var _ringsSlider := $RingsSlider as Slider
onready var _params := {
    "height": [
        _heightSlider,
        $infoLabel1 as Label,
        $valueLabel1 as Label,
        false,
    ],
    "top_radius" : [
        _topRadiusSlider,
        $infoLabel2 as Label,
        $valueLabel2 as Label,
        false,
    ],
    "bottom_radius" : [
        _bottomRadiusSlider,
        $infoLabel3 as Label,
        $valueLabel3 as Label,
        false,
    ],
    "radial_segments" : [
        _radialSegmentsSlider,
        $infoLabel4 as Label,
        $valueLabel4 as Label,
        true,
    ],
    "rings" : [
        _ringsSlider,
        $infoLabel5 as Label,
        $valueLabel5 as Label,
        true,
    ],
   }

func _ready() -> void:
    for value in _params.values():
        var slider := value[0] as Slider
        _update_number(slider.value, value[2], value[3])
        var error = slider.connect(
            "value_changed",
            self,
            "_update_number",
            [value[2], value[3]]
        )
        if error != OK:
            push_error("Failed to connect: %d" % error)


func get_params() -> Dictionary:
    var result: Dictionary
    for key in _params.keys():
        result[key] = (_params[key][0] as Slider).value
    return result


func _update_number(value: float, label: Node, convert_to_int: bool) -> void:
    if convert_to_int:
        label.text = "%d" % int(value)
    else:
        label.text = "%.2f" % value
