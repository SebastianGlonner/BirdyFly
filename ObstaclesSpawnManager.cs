using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Godot;
using Godot.NativeInterop;

public class ObstaclesSpawnManager {
    private float _baseSpeed = 2f;

	private float _speed = 2f;


    private float _appearanceMeter = 4;
    
    private Node world;
    private float startPointX = 6;
    private float endPointX = -8;
    private Vector3 direction = new Vector3(-1, 0, 0);
	private PackedScene pipeScene;
	private FlexibelTimer spawnMeter;

    private List<Node3D> pipes = new();

    private float spawnYRangeMax = 0f;
    // private float spawnYRangeMax = 3f;

    private Random rnd = new();

    public ObstaclesSpawnManager(Node world) {
        this.world = world;
		pipeScene = (PackedScene)ResourceLoader.Load("res://Pipe.tscn");
    }

    public void ready() {
        spawnMeter = new FlexibelTimer(_appearanceMeter, true);
		spawnMeter.TimeoutEvent += (object sender, EventArgs e) => {
            spwan();
		};

        spawnMeter.start();

        this.spwan();
    }

    public void spwan() {
        Node3D pipe = pipeScene.Instantiate<Node3D>();
        pipes.Add(pipe);
        pipe.Position = new Vector3(startPointX, (float)(rnd.NextDouble() * spawnYRangeMax - spawnYRangeMax / 2), 0);
        world.AddChild(pipe);
        removeOldPipes();
    }

    public void process(double delta) {
        Dash.singleton()._process(delta);


		if (Input.IsActionPressed("dash")) {
            Dash.singleton().start();
		}

        spawnMeter._process(delta * _speed);

        this._speed = Dash.singleton().calculateSpeed(this._speed, this._baseSpeed);

        foreach(Node3D pipe in pipes) {
            pipe.Position += direction * (float)(_speed * delta);
        }
    }

    private void removeOldPipes() {
        
        foreach(Node3D pipe in pipes.ToList()) {
            if (pipe.Position.X < endPointX) {
                pipes.Remove(pipe);
                pipe.QueueFree();
            }
        }
    }
}