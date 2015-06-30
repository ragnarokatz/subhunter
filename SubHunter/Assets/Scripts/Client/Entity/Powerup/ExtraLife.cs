using UnityEngine;

public class ExtraLife : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraLife();

        Foundation.Log.Trace("Gain an extra life.");
    }
}