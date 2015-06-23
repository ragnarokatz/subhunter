using System;
using System.Collections.Generic;
using UnityEngine;
using Foundation;

public class Level : MonoBehaviour
{
    public Game Game;

    private bool  isRunning;
    private float elapsedTime;
    private int   length;

    public void StartLevel()
    {
        this.isRunning = true;
        this.elapsedTime = 0f;

        var level = Player.I.Level;
        var levelConfig = ConfigManager.I.GetConfig(String.Format("Level{0}", level));
        this.length = (int) levelConfig["Length"];
    }

    public void PauseLevel()
    {
        this.isRunning = false;
    }

    public void ResumeLevel()
    {
        this.isRunning = true;
    }

    public void EndLevel()
    {
        this.isRunning = false;
    }

    private void Update()
    {
        if (! this.isRunning)
            return;

        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime > this.length)
            this.Game.LevelBreak();
    }
}