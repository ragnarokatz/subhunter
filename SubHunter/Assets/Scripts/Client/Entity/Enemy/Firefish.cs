using UnityEngine;

public class Firefish : Sub
{
    private const float DETECT_RANGE = 1.5f;
    private const float SHOOT_RANGE = 0.05f;

    private float initSpeed;

    protected override void Start ()
    {
        base.Start();

        this.initSpeed = speed;
    }

    protected override void Update ()
    {
        if (this.isExploding)
            return;
        
        MoveUpdate();
        ShootUpdate();
    }

    private void MoveUpdate()
    {
        var dist = Mathf.Abs(this.transform.position.x - GameObserver.PlayerShip.transform.position.x);
        if (dist > DETECT_RANGE)
        {
            this.transform.position += this.dir * this.initSpeed * Time.deltaTime;
            return;
        }

        this.speed = dist / DETECT_RANGE * this.initSpeed;
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void ShootUpdate()
    {
        if (this.Weapon == null)
            return;

        if (Time.time < this.nextShootTime)
            return;
        
        if (Mathf.Abs(this.transform.position.x - GameObserver.PlayerShip.transform.position.x) > SHOOT_RANGE)
            return;

        Shoot();
    }
}