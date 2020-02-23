using UnityEngine;
using Foundation;

public class Highscore
{
    public static int Score { get { return PlayerPrefs.GetInt("high_score", 0); } }

    public static bool TrySubmitHighscore(int score)
    {
        Log.Trace("Submit new score {0} compared to old score {1}.", score, Highscore.Score);

        if (score <= Highscore.Score)
            return false;

        PlayerPrefs.SetInt("high_score", score);
        return true;
    }
}

