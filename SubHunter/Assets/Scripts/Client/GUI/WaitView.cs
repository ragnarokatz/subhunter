using System;
using UnityEngine;
using UnityEngine.UI;

public class WaitView : MonoBehaviour
{
    public Text Score;

    private void OnEnable()
    {
        this.Score.text = Highscore.Score.ToString();
    }
}