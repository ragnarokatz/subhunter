using System;
using Foundation;

public class Player
{
    public delegate void PlayerInfoHandler(string type, int value);
    public event PlayerInfoHandler OnUpdatePlayerInfo;

    private static Player instance = new Player();
    public static Player I { get { return Player.instance; } }

    private int lives;
    private int level;
    private int score;
    private int maxClip;

    public int Lives   { get { return this.lives; } }
    public int Level   { get { return this.level; } }
    public int Score   { get { return this.score; } }
    public int MaxClip { get { return this.maxClip; } }

    public void StartNewGame()
    {
        this.lives   = 5;
        this.level   = 0;
        this.score   = 0;
        this.maxClip = 5;
    }

    public void EndGame()
    {
    }

    public void AdvanceToNextLevel()
    {
        this.level = (this.level + 1) % 20;

        if (this.OnUpdatePlayerInfo != null)
            this.OnUpdatePlayerInfo("Level", this.level);

        Log.Trace("Advance to level {0}.", this.level);
    }

    public void AddScore(int score)
    {
        this.score += score;

        if (this.OnUpdatePlayerInfo != null)
            this.OnUpdatePlayerInfo("Score", this.score);
    }

    public void GainAnExtraLife()
    {
        this.lives++;

        if (this.OnUpdatePlayerInfo != null)
            this.OnUpdatePlayerInfo("Life", this.lives);
    }

    public void LoseALife()
    {
        this.lives--;

        if (this.OnUpdatePlayerInfo != null)
            this.OnUpdatePlayerInfo("Life", this.lives);
    }

    public void GainAnExtraClip()
    {
        this.maxClip++;

        if (this.OnUpdatePlayerInfo != null)
            this.OnUpdatePlayerInfo("Clip", this.maxClip);
    }
}