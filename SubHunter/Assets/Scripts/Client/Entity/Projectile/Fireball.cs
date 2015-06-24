using UnityEngine;

public class Fireball : Projectile
{
    protected override void Start ()
    {
        base.Start();

        this.dir = Vector3.up;
        this.destroyBoundary = Dimensions.TOP_EDGE;
    }

    protected override void Update ()
    {
        base.Update ();

        if (this.transform.position.y > this.destroyBoundary)
            Destroy();
    }
}