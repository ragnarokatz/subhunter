using UnityEngine;

public class ExtraLife : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraLife();
        Notification.I.DisplayMessage("+1 Life");
        base.Effect();
    }
}