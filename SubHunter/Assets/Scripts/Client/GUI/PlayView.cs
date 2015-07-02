using System;
using Foundation;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    public Text     Level;
    public Text     Score;
    public Text     Life;
    public Text     Clip;
    public Animator Levelup;
    public Animator Addscore;
    public Animator LifeChange;
    public Animator AddClip;

    private void Awake()
    {
        EventManager.OnUpdateAttribs += UpdateAttribs;
    }

    private void UpdateAttribs(object[] para)
    {
        var type = para[0] as String;
        var playAnim = (Boolean) para[1];
        UpdateAttrib(type, playAnim);
    }

    private void UpdateAttrib(string type, bool playAnim)
    {
        switch (type)
        {
            case "level":
            this.Level.text = (Player.I.Level + 1).ToString();
            if (playAnim)
                this.Levelup.Play("levelup");
            break;

            case "life":
            this.Life.text = Player.I.Lives.ToString();
            if (playAnim)
                this.LifeChange.Play ("lifechange");
            break;

            case "clip":
            this.Clip.text = Ship.Data.Clip.ToString();
            if (playAnim)
                this.AddClip.Play ("addclip");
            break;

            case "score":
            this.Score.text = Player.I.Score.ToString();
            if (playAnim)
                this.Addscore.Play("scoreshake");
            break;

            default:
            Log.Assert(false, String.Format("Impossible here, wrong attrib {0}.", type));
            break;
        }
    }

    private void OnDestroy()
    {
        EventManager.OnUpdateAttribs -= UpdateAttribs;
    }
}