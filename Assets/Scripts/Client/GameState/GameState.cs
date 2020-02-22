using System;
using UnityEngine;
using Foundation;

public class GameState
{
    private static readonly WaitState  Wait  = new WaitState();
    private static readonly PlayState  Play  = new PlayState();
    private static readonly PauseState Pause = new PauseState();

    private static GameState current;
    public static GameState Current { get { return GameState.current; } }

    public static bool IsInWaitState()
    {
        return GameState.current == GameState.Wait;
    }

    public static bool IsInPlayState()
    {
        return GameState.current == GameState.Play;
    }

    public static bool IsInPauseState()
    {
        return GameState.current == GameState.Pause;
    }

    public static void ChangeToWaitState()
    {
        ChangeState(GameState.Wait);
    }

    public static void ChangeToPlayState()
    {
        ChangeState(GameState.Play);
    }

    public static void ChangeToPauseState()
    {
        ChangeState(GameState.Pause);
    }

    // Expose this method?
    private static void ChangeState(GameState newState)
    {
        if (GameState.current == null)
        {
            GameState.current = newState;
            return;
        }
        
        if (GameState.current.GetType() == newState.GetType())
            return;

        GameState.current.End();
        GameState.current = newState;
        GameState.current.Start();

        Log.Trace ("Game state changed to {0}", newState.GetType().Name);
    }

    protected virtual void Start()
    {
    }

    protected virtual void End()
    {
    }
}

public class WaitState : GameState
{
}

public class PauseState : GameState
{
    protected override void Start ()
    {
        base.Start ();

        Time.timeScale = 0f;
    }
}

public class PlayState : GameState
{
    protected override void Start ()
    {
        base.Start ();

        Time.timeScale = 1f;
    }
}
