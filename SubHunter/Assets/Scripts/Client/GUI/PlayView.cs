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
        Player.I.OnUpdatePlayerInfo += HandleOnUpdatePlayerInfo;
    }

    private void HandleOnUpdatePlayerInfo (string type, int value)
    {
        switch (type)
        {
        case "Level":
            this.Level.text = value.ToString();
            var anim = this.Level.GetComponent<Animator>();
            anim.SetTrigger("Play");
            break;
        case "Score":
            this.Score.text = value.ToString();
            anim = this.Score.GetComponent<Animator>();
            anim.SetTrigger("Play");
            break;
        case "Life":
            this.Life.text = value.ToString();
            anim = this.Life.GetComponent<Animator>();
            anim.SetTrigger("Play");
            break;
        case "Clip":
            this.Clip.text = value.ToString();
            anim = this.Clip.GetComponent<Animator>();
            anim.SetTrigger("Play");
            break;
        default:
            System.Diagnostics.Debug.Assert(false, "Impossible here.");
            break;
        }
    }
}