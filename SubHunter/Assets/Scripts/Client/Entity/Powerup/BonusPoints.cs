using UnityEngine;

public class BonusPoints : Powerup
{
    public override void Effect()
    {
        Player.I.AddScore(2000);

        Foundation.Log.Trace("Gain bonus points.");
    }
}