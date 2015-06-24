using UnityEngine;

public class Bomb : Projectile
{
    protected override void Start ()
    {
        base.Start();

        this.dir = Vector3.down;
        this.destroyBoundary = Dimensions.BOT_EDGE;
    }

    protected override void Update ()
    {
        base.Update ();

        if (this.transform.position.y < this.destroyBoundary)
            Destroy();
    }
}