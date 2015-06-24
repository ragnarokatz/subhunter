using UnityEngine;
using Foundation;

public class Enemy : Entity
{
    public float SpawnFloor;
    public float SpawnCeiling;
    public int   Points;

    protected bool isExploding;

    public void Explode()
    {
        if (this.isExploding)
            return;

        this.isExploding = true;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
    }
}