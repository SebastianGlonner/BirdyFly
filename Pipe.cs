using Godot;
using System;

public partial class Pipe : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode("%ScoreCollider").Connect("body_exited", Callable.From((Node node) => {
			this.Leave(node);
		}));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void Leave(Node node) {
		GlobalEvents.InvokeScore();
	}
}
