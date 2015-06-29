using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    public Text Level;
    public Text Score;
    public Text Life;
    public Text Clip;

    private void Start()
    {
        EventManager.OnUpdate += UpdateAttribs;
    }

    private void UpdateAttribs(string type)
    {
        switch (type)
        {
        case "level":
            this.Level.text = Player.I.Level.ToString();
            break;
        case "life":
            this.Life.text = Player.I.Lives.ToString();
            break;
        case "clip":
            this.Clip.text = Ship.Data.Clip.ToString();
            break;
        case "score":
            this.Score.text = Player.I.Score.ToString();
            break;
        default:
            System.Diagnostics.Debug.Assert(false, "Impossible here.");
            break;
        }
    }
}