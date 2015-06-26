using UnityEngine;

public class Projectile : Entity
{
    protected float destroyBoundary;

    protected override void Start()
    {
        base.Start();

        EntityManager.I.Projectiles.Add(this);
        this.transform.SetParent(EntityManager.I.ProjectileParent, true);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Destroy ()
    {
        base.Destroy ();

        EntityManager.I.Projectiles.Remove(this);
    }
}