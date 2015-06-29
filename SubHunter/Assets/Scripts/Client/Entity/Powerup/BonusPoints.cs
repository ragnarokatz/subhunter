using SubHunter.Powerup;

public class BonusPoints : Powerup
{
    public override void Effect()
    {
        Player.I.AddScore(2000);
    }
}