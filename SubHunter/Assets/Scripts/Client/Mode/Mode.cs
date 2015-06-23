using System;
using UnityEngine;

public class Mode
{
    public static readonly WaitMode  Wait  = new WaitMode();
    public static readonly PlayMode  Play  = new PlayMode();
    public static readonly PauseMode Pause = new PauseMode();

    private static Mode currentMode;
    public static Mode Current { get { return this.currentMode; } }

    public static void ChangeMode(Mode newMode)
    {
        if (Mode.currentMode == null)
        {
            Mode.currentMode = newMode;
            return;
        }
        
        if (Mode.currentMode.GetType() == newMode.GetType())
            return;

        Mode.currentMode.End();
        Mode.currentMode = newMode;
        Mode.currentMode.Start();
    }

    public virtual void Start()
    {
    }

    public virtual void End()
    {
    }
}

public class WaitMode : Mode
{
}

public class PauseMode : Mode
{
    public override void Start ()
    {
        base.Start ();

        Time.timeScale = 0f;
    }
}

public class PlayMode : Mode
{
    public override void Start ()
    {
        base.Start ();

        Time.timeScale = 1f;
    }
}
