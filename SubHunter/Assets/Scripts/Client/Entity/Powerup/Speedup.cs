using UnityEngine;

public class Speedup : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddSpeedupBuff();
        Notification.I.DisplayMessage("SPEED UP");
        base.Effect();
    }
}
