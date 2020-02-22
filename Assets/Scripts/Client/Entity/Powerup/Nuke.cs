using UnityEngine;

public class Nuke : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddNuke();
        Ship.I.UseNuke(); // Current implementation: immediately use nuke upon pick-up
        Notification.I.DisplayMessage("KABOOM");
        base.Effect();
    }
}