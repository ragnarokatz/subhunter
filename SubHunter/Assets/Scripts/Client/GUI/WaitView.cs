using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitView : MonoBehaviour
{
    public Text     Score;
    public Animator StartText;

    private Vector3 startPosition;

    public void StartNewGame()
    {
        if (this.IsInvoking("StartGame"))
            return;

        this.StartText.Play("textflyup");
        this.Invoke("StartGame", 2f);
    }

    private void StartGame()
    {
        Game.I.StartNewGame();
    }

    private void OnEnable()
    {
        this.Score.text = String.Format("HighScore: {0}", Highscore.Score);
    }

    private void Start()
    {
        this.startPosition = this.StartText.transform.position;
    }

    private void OnDisable()
    {
        this.StartText.transform.position = this.startPosition;
    }
}