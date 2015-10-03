using UnityEngine;

public class ExtraClip : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddClip();
        Notification.I.DisplayMessage("+1 Ammo");
        base.Effect();
    }
}