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
        // TODO: GameObject.Instantiate(Powerup); ////----
        base.Destroy ();
    }
}