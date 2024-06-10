using System;
using System.Threading.Tasks.Dataflow;

public class Skill {

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
        if (cooldown.isActive()) {
            return false;
        }
        StartEvent?.Invoke(this, EventArgs.Empty);
        timespan.start();
        return true;
    }

    public Boolean isActive() {
        return timespan.isActive();
    }
}