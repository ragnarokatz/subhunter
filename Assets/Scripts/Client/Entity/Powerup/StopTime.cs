using UnityEngine;

public class StopTime : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddStoptimeBuff();
        Notification.I.DisplayMessage("FREEZE");
        base.Effect();
    }
}