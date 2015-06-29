using SubHunter.Powerup;

public class ExtraLife : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraLife();
    }
}