using System;
using System.Collections.Generic;
using UnityEngine;
using Foundation;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game I { get { return Game.instance; } }

    public Level    Level;
    public Spawner  Spawner;
    public PlayView PlayView;
    public WaitView WaitView;

    public void StartNewGame()
    {
        Log.Trace ("Starting new game.");

        PlayView.gameObject.SetActive(true);
        WaitView.gameObject.SetActive(false);

        GameState.ChangeToPlayState();

        Player.I.StartNewGame();
        Ship.Data.Init();
        InstantiateShip();
        this.Level.StartLevel();
        this.Spawner.StartSpawn();
    }

    public void EndGame()
    {
        Log.Trace("End game.");

        GameState.ChangeToWaitState();
        Highscore.TrySubmitHighscore(Player.I.Score);

        Player.I.EndGame();
        this.Level.EndLevel();
        this.Spawner.FinishSpawn();

        PlayView.gameObject.SetActive(false);
        WaitView.gameObject.SetActive(true);

        Notification.I.DisplayMessage("Game Over");
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
        {
            Invoke("ResumeLevel", 5f);
            Notification.I.DisplayMessage("Resuming");
        }
        else
            Invoke("EndGame", 1f);
    }

    public void LevelBreak()
    {
        Log.Trace("Player has completed level {0}.", Player.I.Level);

        GameState.ChangeToWaitState();
        AudioManager.I.AudioSources[3].Play();

        DestroyShip(false);
        this.Level.EndLevel();
        this.Spawner.FinishSpawn();

        Invoke("AdvanceToNextLevel", 5f);

        Notification.I.DisplayMessage("Preparing Next Level");
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

    private void DestroyShip(bool explosion = true)
    {
        if (explosion)
            Ship.I.Explode();
        else
            Ship.I.Destroy();
    }

    private void Start()
    {
        Game.instance = this;

        GameState.ChangeToWaitState();
    }
}