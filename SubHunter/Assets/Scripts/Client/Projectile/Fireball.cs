using UnityEngine;

public class Fireball : Projectile
{
    public override void Start ()
    {
        this.dir = Vector3.up;
        this.destroyBoundary = Dimensions.TOP_EDGE;
    }

    public override void Update ()
    {
        base.Update ();

        if (this.transform.position.y > this.destroyBoundary)
            Destroy();
    }
}