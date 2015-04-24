using UnityEngine;

public class Missile : Projectile
{
    public float AngleChangeRate;

    public override void Start ()
    {
        this.dir = Game.PlayerShip.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, this.dir.y, 0f));
        this.destroyBoundary = Dimensions.TOP_EDGE;
    }

    public override void Update ()
    {
        base.Update ();

        var change = this.AngleChangeRate * Time.deltaTime;
        if (Mathf.Abs(this.dir.y - Vector3.up.y) > change)
        {
            if (Vector3.up.y < this.dir.y)
                this.dir -= new Vector3(0f, change, 0f);
            else
                this.dir += new Vector3(0f, change, 0f);

            this.transform.rotation = Quaternion.Euler(this.dir);
        }
    }
}