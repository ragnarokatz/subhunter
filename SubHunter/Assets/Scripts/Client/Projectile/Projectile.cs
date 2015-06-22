using UnityEngine;

public class Projectile : Entity
{
    protected float destroyBoundary;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }
}