using UnityEngine;

public class Speedup : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddSpeedupBuff();

        Foundation.Log.Trace("Start speed up effect.");
    }
}
