using UnityEngine;

public class Nuke : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddNuke();

        // Current implementation: immediately use nuke upon pick-up
        Ship.I.UseNuke();

        Foundation.Log.Trace("Pick up a nuke.");

        Notification.I.DisplayMessage("BOOM!");
    }
}