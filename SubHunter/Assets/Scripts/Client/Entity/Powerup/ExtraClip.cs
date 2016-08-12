using UnityEngine;

public class ExtraClip : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddClip();
        Notification.I.DisplayMessage("Ammo +1");
        base.Effect();
    }
}