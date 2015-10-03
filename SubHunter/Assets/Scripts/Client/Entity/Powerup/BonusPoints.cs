using UnityEngine;

public class BonusPoints : Powerup
{
    public override void Effect()
    {
        Player.I.AddScore(2000);
        Notification.I.DisplayMessage("$2000~");
        base.Effect();
    }
}