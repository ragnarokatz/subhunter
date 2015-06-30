using UnityEngine;

public class Sub : Enemy
{
    public GameObject Weapon;
    public float      ShootIntervalMin;
    public float      ShootIntervalMax;

    protected float shootInterval;
    protected float nextShootTime;

    protected override void Start ()
    {
        base.Start();

        var spawnPos = Random.Range(SpawnFloor, SpawnCeiling);
        var dirRandom = Random.Range(0, 2);
        if (dirRandom == 0)
        {
            this.dir = Vector3.right;
            this.transform.position = new Vector3(Dimensions.LEFT_EDGE, spawnPos, this.transform.position.z);
        } else
        {
            this.dir = Vector3.left;
            this.transform.position = new Vector3(Dimensions.RIGHT_EDGE, spawnPos, this.transform.position.z);

            // All directional sprites default are face right
            this.transform.Rotate(0f, 180f, 0f);
        }

        this.shootInterval = Random.Range(this.ShootIntervalMin, this.ShootIntervalMax);
        GenerateNextShootTime();
    }

    protected override void Update ()
    {
        base.Update();

        if (this.isExploding)
            return;
        
        MoveUpdate();
        ShootUpdate();
    }

    protected void Shoot()
    {
        var projectile = GameObject.Instantiate(Weapon) as GameObject;
        projectile.transform.position = this.transform.position;

        GenerateNextShootTime();
    }

    private void GenerateNextShootTime()
    {
        this.nextShootTime = this.shootInterval + Time.time;
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

        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void ShootUpdate()
    {
        if (this.Weapon == null)
            return;

        if (Time.time < this.nextShootTime)
            return;

        Shoot();
    }
}