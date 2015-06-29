using UnityEngine;
using Foundation;

public class Missile : Projectile
{
    public float YChangeRate;

    protected override void Start ()
    {
        base.Start();

        // Init direction targets ship if exists, or target the center point if not.
        // If angle is more than 45 deg away from vertical line, then adjust maximum angle to 45 deg.
        var targetPos = Ship.IsAlive ? Ship.I.transform.position : new Vector3(0f, Dimensions.WATER, 0f);
        if (targetPos.y < targetPos.x)
            targetPos.y = targetPos.x;

        this.dir = (targetPos - this.transform.position).normalized;
        UpdateRotation(this.dir);

        this.destroyBoundary = Dimensions.TOP_EDGE;
    }

    // Direction changes gradually towards vertical over the course of time.
    protected override void Update ()
    {
        base.Update ();

        if (this.transform.position.y > this.destroyBoundary)
        {
            Destroy();
            return;
        }

        var change = this.YChangeRate * Time.deltaTime;
        if (Vector3.up.y - this.dir.y > change)
        {
            var newY = this.dir.y + change;
            var newX = Mathf.Sqrt(1 - (Mathf.Pow(newY, 2f)));
            if (this.dir.x < 0)
                newX = -newX;

            this.dir.x = newX;
            this.dir.y = newY;
        } else
            this.dir = Vector3.up;

        UpdateRotation(this.dir);
    }

    private void UpdateRotation(Vector3 dir)
    {
        // Translation of missile's flying direction into sprite rotation
        var radian = Mathf.Atan2(this.dir.y, this.dir.x);
        var angle = radian / Mathf.PI * 180 - 90f;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}