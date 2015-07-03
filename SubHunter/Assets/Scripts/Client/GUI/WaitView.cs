using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitView : MonoBehaviour
{
    public Text     Score;
    public Animator StartText;

    public void StartNewGame()
    {
        this.StartText.Play("textflyup");
        this.Invoke("StartGame", 2f);
    }

    private void StartGame()
    {
        Game.I.StartNewGame();
    }

    private void OnEnable()
    {
        this.Score.text = Highscore.Score.ToString();
    }
}