using System;
using System.Threading.Tasks.Dataflow;
using Godot;

public class Skill {

    protected bool restartOnRestart = false; // not implemented

    protected FlexibelTimer timespan;
    protected FlexibelTimer cooldown;
    
    public event EventHandler StartEvent;
    public event EventHandler EndEvent;

    public Skill(float timespan, float cooldown) {
        this.timespan = new FlexibelTimer(timespan, false);
        this.cooldown = new FlexibelTimer(cooldown, false);
    }

    public void _process(double delta) {
        if (timespan._process(delta)) {
            EndEvent?.Invoke(this, EventArgs.Empty);
            cooldown.start();
        }

        if (cooldown._process(delta)) {
            // read 
        }
    }

    public bool start() {
        if (timespan.isActive() || cooldown.isActive()) {
            return false;
        }
        StartEvent?.Invoke(this, EventArgs.Empty);
        timespan.start();
        return true;
    }

    public Boolean isActive() {
        return timespan.isActive();
    }

    public float cooldownPercent() {
        return cooldown.TimeoutPercent();
    }
}