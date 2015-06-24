using UnityEngine;

public class Powerup : Entity
{
    protected float destroyBoundary;

    protected override void Start()
    {
        base.Start();

        this.destroyBoundary = Dimensions.WATER;
        this.dir = Vector3.up;
    }

    protected override void Update()
    {
        if (this.transform.position.y > this.destroyBoundary)
        {
            StartDelay();
        }

        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void StartDelay()
    {
    }

    public virtual void Effect()
    {
    }
}

public class ExtraLife : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraLife();
    }
}

public class ExtraClip : Powerup
{
    public override void Effect()
    {
        Player.I.GainAnExtraClip();
    }
}

public class StopTime : Powerup
{
    public override void Effect()
    {
        MyTime.PauseTime(10f);
    }
}

public class BonusPoints : Powerup
{
    public override void Effect()
    {
        Player.I.AddScore(2000);
    }
}

public class Nuke : Powerup
{
    public override void Effect()
    {
        Game.I.Ship.AddNuke();
    }
}

public class Speedup : Powerup
{
    public override void Effect()
    {
        Game.I.Ship.AddBuff(Speedup);
    }
}

public class Invulnerability : Powerup
{
    public override void Effect()
    {
        Game.I.Ship.AddBuff(Invulnerability);
    }
}