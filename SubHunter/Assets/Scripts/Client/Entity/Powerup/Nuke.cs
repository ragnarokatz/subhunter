using SubHunter.Powerup;

public class Nuke : Powerup
{
    public override void Effect()
    {
        Ship.Data.AddNuke();
    }
}