using UnityEngine;

public class Torpedo : Projectile
{
    // Delay time after torpedo has reached the water surface before destroy
    public float DestroyDelay;

    // For setting delay mode and start time
    private float delayStartTime;
    private bool isInDelay;

    public override void Start ()
    {
        this.dir = Vector3.up;
        this.destroyBoundary = Dimensions.WATER;
    }

    public override void Update ()
    {
        base.Update ();

        if (this.isInDelay)
        {
            // If in delay mode
            if (Time.time - this.delayStartTime < this.DestroyDelay)
                // If the delay time has not been reached yet
                return;

            Destroy();
            return;
        }

        if (this.transform.position.y > this.destroyBoundary)
        {
            // If the torpedo has reached the water surface
            // Set torpedo in delay mode
            this.isInDelay = true;
            this.delayStartTime = Time.time;

            // Stop the torpedo from moving
            this.Speed = 0f;
            this.dir = Vector3.zero;
            return;
        }
    }
}