using System;
using Foundation;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    public Text Level;
    public Text Score;
    public Text Life;
    public Text Clip;

    private void Awake()
    {
        EventManager.OnUpdateAttribs += UpdateAttribs;
    }

    private void UpdateAttribs(object type)
    {
        if (type is Array)
        {
            var arr = type as Array;
            foreach (String t in arr)
                UpdateAttrib(t);
            return;
        }

        if (type is String)
        {
            UpdateAttrib(type as String);
            return;
        }

        Log.Assert(false, String.Format("Impossible here, wrong type {0}.", type.GetType()));
    }

    private void UpdateAttrib(string type)
    {
        switch (type)
        {
            case "level":
            this.Level.text = (Player.I.Level + 1).ToString();
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
            Log.Assert(false, String.Format("Impossible here, wrong attrib {0}.", type));
            break;
        }
    }

    private void OnDestroy()
    {
        EventManager.OnUpdateAttribs -= UpdateAttribs;
    }
}