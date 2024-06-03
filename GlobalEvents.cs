using System;

public class GlobalEvents {
    static public event EventHandler DyingEvent;
    static public void InvokeDying() {
        DyingEvent?.Invoke(typeof(GlobalEvents), EventArgs.Empty);
    }

    
    static public event EventHandler ScoreEvent;
    static public void InvokeScore() {
        ScoreEvent?.Invoke(typeof(GlobalEvents), EventArgs.Empty);
    }
}