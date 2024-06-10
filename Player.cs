using Godot;
using System;

public partial class Player : CharacterBody3D
{

	[Signal] public delegate void HitPipeEventHandler();

	private float _speed = 3f;
	private float accelaration = 0.1f;

	private Vector3 clampGround = new Vector3(0, -2, 0);
	private Vector3 clampAir = new Vector3(0, 2, 0);

	private bool isDashing = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Dash.singleton().StartEvent += (object sender, EventArgs e) => {
			isDashing = false;
			SetCollisionMaskValue(1, true);
		};
        Dash.singleton().EndEvent  += (object sender, EventArgs e) => {
			isDashing = true;
			SetCollisionMaskValue(1, false);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector3 direction = this.getKeyboardDirection();

		Velocity = Velocity.Lerp(direction * this._speed, accelaration);
		
		var collision3D = MoveAndCollide(Velocity * (float)delta);
		if (collision3D != null && !isDashing) {
			this.kinematicCollide(collision3D);
		}
		
		Position = Position.Clamp(clampGround, clampAir);
    }
	

	private Vector3 getKeyboardDirection()
	{
		Vector3 direction = Vector3.Zero;

		if (Input.IsActionPressed("fly_up"))
		{
			direction.Y += 1;
		} else {
			// direction.Y -= 1;
		}
		
		return direction;
	}

	private void kinematicCollide(KinematicCollision3D collision3D) {
		EmitSignal(SignalName.HitPipe);
		
		GlobalEvents.InvokeDying();
	}

}
