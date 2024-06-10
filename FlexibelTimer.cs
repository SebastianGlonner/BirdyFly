using System;

public class FlexibelTimer {
    private float currentTime = 0;
    private float timeout = 0;
    private float halftime = 0;

    private bool inHalftime = false;

    private bool active = false;
    private bool iterative = false;
    
    public event EventHandler TimeoutEvent;

    public FlexibelTimer(float timeout, bool iterative) {
        this.timeout = timeout;
        this.iterative = iterative;
        this.halftime = timeout / 2;
    }
    
    private void InvokeTimeout() {
        TimeoutEvent?.Invoke(typeof(GlobalEvents), EventArgs.Empty);
    }


    // return true if timeout triggers, does not stop timer but restarts it 
    public bool _process(double delta) {
        if (!active) {
            return false;
        }

        this.currentTime += (float) delta;
        inHalftime = currentTime >= halftime;
        if (this.currentTime >= timeout) {
            this.InvokeTimeout();
            this.currentTime = 0;
            this.inHalftime = false;

            if (!iterative) {
                this.stop();
            }

            return true;
        }

        return false;
    }

    public void stop() {
        this.currentTime = 0;
        this.inHalftime = false;
        this.active = false;
    }

    public void start() {
        this.currentTime = 0;
        this.inHalftime = false;
        this.active = true;
    }

    public bool isActive() {
        return active;
    }

    public bool overHalftime() {
        return inHalftime;
    }
}