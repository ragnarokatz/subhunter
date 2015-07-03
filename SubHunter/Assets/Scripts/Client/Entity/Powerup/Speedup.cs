using UnityEngine;

public class Speedup : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddSpeedupBuff();

        Foundation.Log.Trace("Start speedup effect.");

        Notification.I.DisplayMessage("I'm like the wind.");
    }
}
