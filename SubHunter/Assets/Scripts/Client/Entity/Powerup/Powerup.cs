using UnityEngine;

public class Powerup : Entity
{
    protected float destroyBoundary;

    protected override void Start()
    {
        base.Start();

        this.dir = Vector3.up;
    }

    protected override void Update()
    {
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }
}