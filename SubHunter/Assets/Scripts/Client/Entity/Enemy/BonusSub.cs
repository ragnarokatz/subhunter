using UnityEngine;

public class BonusSub : Sub
{
    protected override void Start ()
    {
        base.Start();
    }

    protected override void Update ()
    {
        base.Update();
    }

    public override void Destroy ()
    {
        var powerup = Prefabs.GetRandomPowerup();
        GameObject.Instantiate(powerup, this.transform.position, Quaternion.identity);

        base.Destroy ();
    }
}