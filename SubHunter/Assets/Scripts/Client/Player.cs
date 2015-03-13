using System;

public class Player
{
    public static int Lives { get; set; }
    public static int Level { get; set; }
    public static int Completion { get; set; }
    public static int Score { get; set; }
    public static int Clip { get; set; }

    public static void AddScore(int score)
    {
        Player.Score += score;
    }
}