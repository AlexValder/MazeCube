extends Label


func _process(_delta: float) -> void:
    self.text = "%.2f fps" % Engine.get_frames_per_second()
