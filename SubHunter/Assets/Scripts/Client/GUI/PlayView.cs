using System;
using Foundation;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    private const float DURATION = 0.4f;

    public Text     Level;
    public Text     Score;
    public Text     Life;
    public Text     Clip;
    // public Animator Levelup;
    // public Animator Addscore;
    // public Animator LifeChange;
    // public Animator AddClip;
    // public Animator RestoreClip;
    // public AudioSource LevelupSound;

    private bool  isMovingLeft;
    private bool  isMovingRight;
    private bool  isFiringLeft;
    private bool  isFiringRight;
    private bool  isFiringMiddle;
    private bool  isAddingScore;
    private float addScoreRate;

    public void OnStartMoveLeft()
    {
        this.isMovingLeft = true;
    }

    public void OnEndMoveLeft()
    {
        this.isMovingLeft = false;
    }

    public void OnStartMoveRight()
    {
        this.isMovingRight = true;
    }

    public void OnEndMoveRight()
    {
        this.isMovingRight = false;
    }

    public void OnStartFireLeft()
    {
        this.isFiringLeft = true;
    }

    public void OnEndFireLeft()
    {
        this.isFiringLeft = false;
    }
    
    public void OnStartFireRight()
    {
        this.isFiringRight = true;
    }
    
    public void OnEndFireRight()
    {
        this.isFiringRight = false;
    }

    public void OnStartFireMiddle()
    {
        this.isFiringMiddle = true;
    }

    public void OnEndFireMiddle()
    {
        this.isFiringMiddle = false;
    }

    private void Awake()
    {
        EventManager.OnUpdateAttribs += UpdateAttribs;
    }

    private void Update()
    {
        if (this.isAddingScore)
            UpdateScore();

        if (! Ship.IsAlive)
            return;

        if (this.isFiringLeft)
            Ship.I.FireLeft();

        if (this.isFiringRight)
            Ship.I.FireRight();

        if (this.isFiringMiddle)
            Ship.I.FireMiddle();

        if (this.isMovingLeft)
            Ship.I.MoveLeft();

        if (this.isMovingRight)
            Ship.I.MoveRight();
    }

    private void UpdateAttribs(object[] para)
    {
        var type = para[0] as String;
        var playAnim = (Boolean) para[1];
        var animType = para[2] as String;
        UpdateAttrib(type, playAnim, animType);
    }

    private void UpdateAttrib(string type, bool playAnim, string animType)
    {
        switch (type)
        {
        case "level":
            this.Level.text = (Player.I.Level + 1).ToString();
            if (playAnim)
            {
                // this.Levelup.Play("levelup");
                // this.LevelupSound.Play();
            }
            break;

        case "life":
            this.Life.text = Player.I.Lives.ToString();
            if (playAnim)
            {
                // this.LifeChange.Play ("lifechange");
            }

            break;

        case "clip":
            this.Clip.text = Ship.Data.Clip.ToString();
            if (! playAnim)
                break;

            if (animType == "add")
            {
                // this.AddClip.Play ("addclip");
            } else if (animType == "restore")
            {
                // this.RestoreClip.Play ("restoreclip");
            } else
                Log.Assert(false, "Unrecognized anim type {0}.");

            break;

        case "score":
            if (! playAnim)
            {
                this.Score.text = Player.I.Score.ToString();
                return;
            }

            // this.Addscore.Play("scoreshake");

            var currentScore = 0;
            if (! Int32.TryParse(this.Score.text, out currentScore))
            {
                this.Score.text = Player.I.Score.ToString();
                return;
            }

            if (currentScore >= Player.I.Score)
            {
                this.Score.text = Player.I.Score.ToString();
                return;
            }

            this.isAddingScore = true;
            this.addScoreRate = (Player.I.Score - currentScore) / PlayView.DURATION;

            break;

        default:
            Log.Assert(false, String.Format("Impossible here, wrong attrib {0}.", type));
            break;
        }
    }

    private void UpdateScore()
    {
        var currentScore = Int32.Parse(this.Score.text);
        currentScore += (int) (this.addScoreRate * Time.deltaTime + 1);
        // ATTENION: WHY ADD 1? To force a change in score when the change is too little (< 1).

        if (currentScore > Player.I.Score)
        {
            currentScore = Player.I.Score;
            this.isAddingScore = false;
        }

        this.Score.text = currentScore.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnUpdateAttribs -= UpdateAttribs;
    }
}