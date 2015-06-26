using UnityEngine;

public class Firefish : Sub
{
    private const float DETECT_RANGE = 1.5f;
    private const float SHOOT_RANGE = 0.15f;

    private float initSpeed;

    public override void Destroy ()
    {
        base.Destroy ();
    }

    protected override void Start ()
    {
        base.Start();

        this.initSpeed = speed;
    }

    protected override void Update ()
    {
        if (this.isExploding)
        {
            if (Time.time - this.explodeStartTime < Enemy.EXPLODE_DURATION)
                return;

            Destroy();
            return;
        }

        MoveUpdate();
        ShootUpdate();
    }

    private void MoveUpdate()
    {
        if (this.dir == Vector3.right && this.transform.position.x > Dimensions.RIGHT_EDGE)
        {
            Destroy();
            return;
        }
        
        if (this.dir == Vector3.left && this.transform.position.x < Dimensions.LEFT_EDGE)
        {
            Destroy();
            return;
        }

        if (! Ship.IsAlive)
        {
            this.transform.position += this.dir * this.speed * Time.deltaTime;
            return;
        }

        var dist = Mathf.Abs(this.transform.position.x - Ship.I.transform.position.x);
        if (dist <= DETECT_RANGE)
        {
            this.transform.position += this.dir * this.speed * Time.deltaTime;
            return;
        }

        this.speed = dist / DETECT_RANGE * this.initSpeed;
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void ShootUpdate()
    {
        if (this.Weapon == null)
            return;

        if (! Ship.IsAlive)
            return;

        if (Time.time < this.nextShootTime)
            return;
        
        if (Mathf.Abs(this.transform.position.x - Ship.I.transform.position.x) > SHOOT_RANGE)
            return;

        Shoot();
    }
}