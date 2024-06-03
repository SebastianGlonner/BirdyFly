using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ObstaclesSpawnManager {
	private float _speed = 2f;

    private float _appearanceSeconds = 2;
    
    private Node world;
    private float startPointX = 6;
    private float endPointX = -8;
    private Vector3 direction = new Vector3(-1, 0, 0);
	private PackedScene pipeScene;
	private Timer spawnTimer = new();

    private List<Node3D> pipes = new();

    private float spawnYRangeMax = 3f;
    private Random rnd = new();

    public ObstaclesSpawnManager(Node world) {
        this.world = world;
		pipeScene = (PackedScene)ResourceLoader.Load("res://Pipe.tscn");
    }

    public void ready() {
		spawnTimer.Connect("timeout", Callable.From(() => {
			this.spwan();
		}));

		world.AddChild(spawnTimer);
		this.spawnTimer.WaitTime = _appearanceSeconds;
		// this.spawnTimer.OneShot = true;
		this.spawnTimer.Start();
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