﻿using UnityEngine;
using Foundation;

public class Bomb : Projectile
{
    protected override void Start ()
    {
        this.gameObject.name = this.GetType().Name;
        Log.Trace("Instantiate entity {0}.", this.gameObject.name);

        this.speed = Random.Range(SpeedMin, SpeedMax);
        this.dir = Vector3.down;
        this.destroyBoundary = Dimensions.BOT_EDGE;

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

        Game.I.Ship.AddClip();
        EntityManager.I.Bombs.Remove(this);
    }
}