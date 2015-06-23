using System;

public class Player
{
    private static Player instance = new Player();
    public static Player I { get { return Player.instance; } }

    private int lives;
    private int level;
    private int score;
    private int maxClip;

    public int Lives { get { return this.lives; } }
    public int Level { get { return this.level; } }
    public int Score { get { return this.score; } }
    public int MaxClip { get { return this.maxClip;  } }

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
        this.level++ % 20;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void GainAnExtraLife()
    {
        this.lives++;
    }

    public void LoseALife()
    {
        this.lives--;
    }

    public void GainAnExtraClip()
    {
        this.maxClip++;
    }
}