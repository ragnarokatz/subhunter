using UnityEngine;

public class Speedup : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddSpeedupBuff();
        Notification.I.DisplayMessage("LIGHT SPEED");
        base.Effect();
    }
}
