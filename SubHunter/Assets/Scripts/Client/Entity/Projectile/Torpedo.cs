using UnityEngine;

public class Torpedo : Projectile
{
    public float DestroyDelay;

    private float delayStartTime;
    private bool  isInDelay;

    protected override void Start ()
    {
        base.Start();

        this.dir = Vector3.up;
        this.destroyBoundary = Dimensions.WATER;
    }

    protected override void Update ()
    {
        base.Update ();

        if (this.isInDelay)
        {
            if (Time.time - this.delayStartTime < this.DestroyDelay)
                return;

            Destroy();
            return;
        }

        if (this.transform.position.y > this.destroyBoundary)
        {
            this.isInDelay = true;
            this.delayStartTime = Time.time;
            this.speed = 0f;
            this.dir = Vector3.zero;
            return;
        }
    }
}