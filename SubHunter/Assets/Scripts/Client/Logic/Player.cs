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

        EventManager.UpdateAttribs("score", false);
        EventManager.UpdateAttribs("level", false);
        EventManager.UpdateAttribs("life", false);
    }

    public void EndGame()
    {
    }

    public void AdvanceToNextLevel()
    {
        this.level = (this.level + 1) % 15;
        EventManager.UpdateAttribs("level", true);

        Log.Trace("Advance to level {0}.", this.level);
    }

    public void AddScore(int score)
    {
        this.score += score;
        EventManager.UpdateAttribs("score", true);
    }

    public void GainAnExtraLife()
    {
        this.lives++;
        EventManager.UpdateAttribs("life", true);
    }

    public void LoseALife()
    {
        this.lives--;
        EventManager.UpdateAttribs("life", true);
    }
}