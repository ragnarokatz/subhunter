using System;
using Foundation;

public class Player
{
    private static Player instance = new Player();
    public static Player I { get { return Player.instance; } }

    private int lives;
    private int level;
    private int score;

    public int Lives   { get { return this.lives; } }
    public int Level   { get { return this.level; } }
    public int Score   { get { return this.score; } }

    public void StartNewGame()
    {
        this.lives = 5;
        this.level = 0;
        this.score = 0;

        EventManager.UpdateAttrib("score");
        EventManager.UpdateAttrib("level");
        EventManager.UpdateAttrib("life");
    }

    public void EndGame()
    {
    }

    public void AdvanceToNextLevel()
    {
        this.level = (this.level + 1) % 20;
        EventManager.UpdateAttrib("level");

        Log.Trace("Advance to level {0}.", this.level);
    }

    public void AddScore(int score)
    {
        this.score += score;
        EventManager.UpdateAttrib("score");
    }

    public void GainAnExtraLife()
    {
        this.lives++;
        EventManager.UpdateAttrib("life");
    }

    public void LoseALife()
    {
        this.lives--;
        EventManager.UpdateAttrib("life");
    }
}