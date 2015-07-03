using UnityEngine;

public class ExtraClip : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddClip();

        Foundation.Log.Trace("Gain an extra clip.");

        Notification.I.DisplayMessage("Extra ammo.");
    }
}