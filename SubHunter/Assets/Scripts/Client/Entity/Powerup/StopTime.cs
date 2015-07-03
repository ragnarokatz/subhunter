using UnityEngine;

public class StopTime : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddStoptimeBuff();

        Foundation.Log.Trace("Start stop time effect.");

        Notification.I.DisplayMessage("Freeze!");
    }
}