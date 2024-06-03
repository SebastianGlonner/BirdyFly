using Godot;
using System;

public partial class Player : CharacterBody3D
{

	[Signal] public delegate void HitPipeEventHandler();

	private float _speed = 3f;
	private float accelaration = 0.1f;

	private Vector3 clampGround = new Vector3(0, -2, 0);
	private Vector3 clampAir = new Vector3(0, 2, 0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector3 direction = this.getKeyboardDirection();

		Velocity = Velocity.Lerp(direction * this._speed, accelaration);
		// Position += movement;
		// Position = Position.Clamp(clampGround, clampAir);

		var collision3D = MoveAndCollide(Velocity * (float)delta);
		if (collision3D != null) {
			this.kinematicCollide(collision3D);
		}
    }
	

	private Vector3 getKeyboardDirection()
	{
		Vector3 direction = Vector3.Zero;

		if (Input.IsActionPressed("fly_up"))
		{
			direction.Y += 1;
		} else {
			direction.Y -= 1;
		}
		
		return direction;
	}

	private void kinematicCollide(KinematicCollision3D collision3D) {
		EmitSignal(SignalName.HitPipe);
		
		GlobalEvents.InvokeDying();
	}
}
