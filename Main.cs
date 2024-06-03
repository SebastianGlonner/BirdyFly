using Godot;
using System;

public partial class Main : Node
{

	private int _score = 0;

	private Node _currentGame = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		(GetNode("%IgMenu") as IgMenu).Pause += PauseGame;
		(GetNode("%IgMenu") as IgMenu).Start += StartGame;
		PauseGame();

		GlobalEvents.DyingEvent += Dying;
		GlobalEvents.ScoreEvent += (object sender, EventArgs e) => {
			_score++;
			UpdateScore();
		};
	}

	private void Dying(object sender, EventArgs e) {
		this._currentGame.Free();
		this._currentGame = null;
		(GetNode("%IgMenu") as IgMenu).restart();
	}

	private void UpdateScore() {
		(GetNode("%IgMenu") as IgMenu).updateScore("" + _score);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void StartGame() {
		if (this._currentGame != null) {
			this._currentGame.Free();
		}

		this._currentGame = ((PackedScene)ResourceLoader.Load("res://Game.tscn")).Instantiate();
		this.AddChild(_currentGame);
		GetTree().Paused = false;
		_score = 0;
		UpdateScore();
	}

	private void PauseGame() {
		GetTree().Paused = !GetTree().Paused;
		(GetNode("%IgMenu") as IgMenu).setMessage(GetTree().Paused ? "Pausiert" : "");
	}
}
