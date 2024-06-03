using Godot;
using System;
using System.Data;

public partial class IgMenu : CanvasLayer
{

	[Signal] public delegate void PauseEventHandler();
	[Signal] public delegate void StartEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void restart() {
		((CanvasLayer) GetNode("%StartMenu")).Show();
	}

	public void _on_pause_button_button_up() {
		EmitSignal(SignalName.Pause);
	}

	public void _on_start_button_button_up() {
		EmitSignal(SignalName.Start);
		((CanvasLayer) GetNode("%StartMenu")).Hide();
	}

	public override void _Input(InputEvent @event) {
		if (@event.IsActionPressed("pause")) {
			_on_pause_button_button_up();
		}
	}

	public void setMessage(String msg) {
		((Label)GetNode("%Message")).Text = msg;
	}

	public void updateScore(String score) {
		((Label)GetNode("%Score")).Text = score;
	}
}
