using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game I { get { return Game.instance; } }

    public Level   Level;
    public Spawner Spawner;

    public void StartNewGame()
    {
        GameState.ChangeToPlayState();

        MyTime.StartTime();

        Player.I.StartNewGame();
        InstantiateShip();
        this.Level.StartLevel();
        this.Spawner.StartSpawn();
    }

    public void EndGame()
    {
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