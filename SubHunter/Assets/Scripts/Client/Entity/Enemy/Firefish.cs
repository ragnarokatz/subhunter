using UnityEngine;

public class Firefish : Sub
{
    private const float DETECT_RANGE = 1.5f;
    private const float SHOOT_RANGE = 0.15f;

    private float initSpeed;

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
        if (Game.I.Ship == null)
        {
            this.transform.position += this.dir * this.speed * Time.deltaTime;
            return;
        }

        var dist = Mathf.Abs(this.transform.position.x - Game.I.Ship.transform.position.x);
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

        if (Game.I.Ship == null)
            return;

        if (Time.time < this.nextShootTime)
            return;
        
        if (Mathf.Abs(this.transform.position.x - Game.I.Ship.transform.position.x) > SHOOT_RANGE)
            return;

        Shoot();
    }
}