using UnityEngine;
using Foundation;

public class Enemy : Entity
{
    public float SpawnCeiling;
    public float SpawnFloor;
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