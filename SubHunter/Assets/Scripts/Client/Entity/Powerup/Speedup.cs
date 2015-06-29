using SubHunter.Powerup;

public class Speedup : Powerup
{
    public override void Effect()
    {
        BuffManager.I.AddSpeedupBuff();
    }
}