using UnityEngine;
using Foundation;

public class Bomb : Projectile
{
    protected override void Start ()
    {
        this.speed = Random.Range(SpeedMin, SpeedMax);
        this.dir = Vector3.down;
        this.destroyBoundary = Dimensions.BOTTOM_EDGE;

        EntityManager.I.Bombs.Add(this);
        this.transform.SetParent(EntityManager.I.ProjectileParent, true);
    }

    protected override void Update ()
    {
        base.Update ();

        if (this.transform.position.y < this.destroyBoundary)
            Destroy();
    }

    public override void Destroy ()
    {
        GameObject.Destroy(this.gameObject);

        Ship.Data.RestoreClip();
        EntityManager.I.Bombs.Remove(this);
    }
}