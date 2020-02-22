using UnityEngine;
using Foundation;
using CodeStage.AntiCheat.ObscuredTypes;

public class Highscore
{
    public static int Score { get { return ObscuredPrefs.GetInt("high_score", 0); } }

    public static bool TrySubmitHighscore(int score)
    {
        Log.Trace("Submit new score {0} compared to old score {1}.", score, Highscore.Score);

        if (score <= Highscore.Score)
            return false;

        ObscuredPrefs.SetInt("high_score", score);
        return true;
    }
}

