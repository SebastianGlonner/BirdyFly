using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Godot;

public class Dash : Skill {

    static private Dash instance;
    static public Dash singleton() {
        if (instance == null) {
            Dash.reset();
        }
        return instance;
    }
    static public void reset() {
        Dash.instance = new(0.2f, 3.5f);
    }

    public float dashSpeed = 30f;
    public float dashExcelaration = 0.7f;

    public Dash(float timespan, float cooldown) : base(timespan, cooldown)
    {
    }

    public float calculateSpeed(float currentSpeed, float baseSpeed) {
        if (timespan.overHalftime()) {
            return Mathf.Lerp(baseSpeed, currentSpeed, dashExcelaration);
        }

        if (isActive()) {
            return Mathf.Lerp(currentSpeed, this.dashSpeed, dashExcelaration);
        }

        return baseSpeed;
    }
}