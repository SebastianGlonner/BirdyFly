using Godot;
using System;
using System.Data;

public partial class IgMenu : CanvasLayer
{

	[Signal] public delegate void PauseEventHandler();
	[Signal] public delegate void StartEventHandler();

	private ProgressBar dashCooldownProgress;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		((CanvasLayer) GetNode("%GameMenu")).Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		if (dashCooldownProgress != null) {
        	dashCooldownProgress.Value = Dash.singleton().cooldownPercent();
		}
    }

    public void restart() {
		((CanvasLayer) GetNode("%StartMenu")).Show();
		((CanvasLayer) GetNode("%GameMenu")).Hide();
		dashCooldownProgress = null;
	}

	public void _on_pause_button_button_up() {
		EmitSignal(SignalName.Pause);
	}

	public void _on_start_button_button_up() {
		EmitSignal(SignalName.Start);
		((CanvasLayer) GetNode("%StartMenu")).Hide();
		((CanvasLayer) GetNode("%GameMenu")).Show();
		dashCooldownProgress = ((ProgressBar) GetNode("%DashProgressBar"));
	}

	public void _on_dash_button_button_up() {
        Dash.singleton().start();
		((Control) GetNode("%DashButton")).AcceptEvent();
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
