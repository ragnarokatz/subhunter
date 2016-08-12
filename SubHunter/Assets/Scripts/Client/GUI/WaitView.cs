using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitView : MonoBehaviour
{
    public Text Score;
    public Animator StartText;

    private Vector3 startPosition;

    public void StartNewGame()
    {
        if (this.IsInvoking("StartGame"))
            return;

        this.StartText.enabled = true;
        this.Invoke("StartGame", 1.8f);
    }

    private void StartGame()
    {
        Game.I.StartNewGame();
    }

    private void OnEnable()
    {
        this.Score.text = Highscore.Score.ToString();
    }

    private void OnDisable()
    {
        this.StartText.enabled = false;
    }
}