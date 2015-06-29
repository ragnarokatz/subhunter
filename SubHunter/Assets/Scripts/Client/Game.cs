using System;
using System.Collections.Generic;
using UnityEngine;
using Foundation;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game I { get { return Game.instance; } }

    public Level   Level;
    public Spawner Spawner;

    public void StartNewGame()
    {
        Log.Trace ("Starting new game.");

        GameState.ChangeToPlayState();

        MyTime.StartTime();

        Player.I.StartNewGame();
        InstantiateShip();
        Ship.Data.Clip = Player.I.MaxClip;
        this.Level.StartLevel();
        this.Spawner.StartSpawn();
    }

    public void EndGame()
    {
        Log.Trace("End game.");

        GameState.ChangeToWaitState();

        Player.I.EndGame();
        DestroyShip();
        this.Level.EndLevel();
        this.Spawner.FinishSpawn();

        Highscore.TrySubmitHighscore(Player.I.Score);
    }

    public void PauseGame()
    {
        GameState.ChangeToPauseState();
    }

    public void ResumeGame()
    {
        GameState.ChangeToPlayState();
    }

    public void DieBreak()
    {
        Log.Trace("Player died.");

        GameState.ChangeToWaitState();

        DestroyShip();
        Player.I.LoseALife();

        if (Player.I.Lives > 0)
            Invoke("ResumeLevel", 5f);
        else
            Invoke("EndGame", 2f);
    }

    public void LevelBreak()
    {
        Log.Trace("Player has completed level {0}.", Player.I.Level);

        GameState.ChangeToWaitState();

        DestroyShip();
        this.Level.EndLevel();
        this.Spawner.FinishSpawn();

        Invoke("AdvanceToNextLevel", 5f);
    }

    private void ResumeLevel()
    {
        GameState.ChangeToPlayState();

        InstantiateShip();
    }

    private void AdvanceToNextLevel()
    {
        GameState.ChangeToPlayState();

        MyTime.StartTime();

        InstantiateShip();
        Player.I.AdvanceToNextLevel();
        this.Level.StartLevel();
        this.Spawner.StartSpawn();
    }

    private void InstantiateShip()
    {
        GameObject.Instantiate(Prefabs.Ship);
    }

    private void DestroyShip()
    {
        Ship.I.Destroy();
    }

    private void Start()
    {
        Game.instance = this;

        GameState.ChangeToWaitState();
    }

    private void Update()
    {
    }
}