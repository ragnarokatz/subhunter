using UnityEngine;

public class ExtraLife : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraLife();
        Notification.I.DisplayMessage("Life +1");
        base.Effect();
    }
}