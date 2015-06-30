using SubHunter.Powerup;

public class ExtraClip : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddClip();
    }
}