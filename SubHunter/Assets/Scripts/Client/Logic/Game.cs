using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Ship       Ship;
    public Level      Level;
    public ObjSpawner ObjSpawner;

    public void StartNewGame()
    {
        this.Ship = GameObject.Instantiate(ObjSpawner.Ship);

        Player.I.StartNewGame();
        this.Level.StartLevel();
        this.ObjSpawner.StartSpawn();
    }

    public void EndGame()
    {
        this.Ship.Destroy();
        this.Ship = null;

        Player.I.EndGame();
        this.Level.EndLevel();
        this.ObjSpawner.FinishSpawn();

        HighscoreConfig.I.TrySubmitHighscore(Player.I.Score);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        this.Level.PauseLevel();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.Level.ResumeLevel();
    }

    public void DieBreak()
    {
        this.Ship.Destroy();
        this.Ship = null;

        this.Level.PauseLevel();
        Invoke("ResumeLevel", 5f);
    }

    public void LevelBreak()
    {
        this.Ship.Destroy();
        this.Ship = null;

        this.Level.EndLevel();
        Invoke("AdvanceToNextLevel", 5f);
    }

    private void ResumeLevel()
    {
        this.Ship = GameObject.Instantiate(ObjSpawner.Ship);

        this.Level.ResumeLevel();
    }

    private void AdvanceToNextLevel()
    {
        this.Ship = GameObject.Instantiate(ObjSpawner.Ship);

        Player.I.AdvanceToNextLevel();
        this.Level.StartLevel();
    }
}