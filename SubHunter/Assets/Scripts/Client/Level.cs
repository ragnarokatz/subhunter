using System;
using System.Collections.Generic;
using UnityEngine;
using Foundation;

[RequireComponent(typeof(Game))]
public class Level : MonoBehaviour
{
    private float startTime;
    private int   length;
    private bool  isRunning;

    public void StartLevel()
    {
        var level = Player.I.Level;
        var levelConfig = ConfigManager.I.GetConfig(String.Format("Level{0}", level));
        this.length = (int) levelConfig["Length"];

        this.startTime = MyTime.time;
        this.isRunning = true;

        Log.Trace("Starting level {0}.", level);
    }

    public void EndLevel()
    {
        this.isRunning = false;
    }

    private void Update()
    {
        if (! this.isRunning)
            return;
        
        if (MyTime.time - this.startTime > this.length)
            Game.I.LevelBreak();
    }
}