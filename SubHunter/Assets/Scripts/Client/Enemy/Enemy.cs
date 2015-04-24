using UnityEngine;
using Foundation;

public enum Motions
{
    Linear,
    Accelerated,
    CreepUp,
}

public enum ShootStyles
{
    Interval,
    InRange,
}

public enum Limits
{
    TopEdge,
    WaterSurface
}

public class Enemy : MonoBehaviour
{
    // Enemy ship dimensions
    public float Width;
    public float Height;

    // Speed
    public float SpeedMin;
    public float SpeedMax;

    // Spawn locations
    public float SpawnCeiling;
    public float SpawnFloor;

    // Points awarded upon destruction
    public int Points;

    // Enemy's weapon
    public GameObject Weapon;

    // Collision
    protected Rect Box
    {
        get
        {
            return new Rect(
                this.transform.position.x - this.Width / 2,
                this.transform.position.y - this.Height / 2,
                this.Width,
                this.Height);
        }
    }

    // Movement
    protected float speed;
    protected Vector3 dir;

    // Shoot
    protected float nextShootTime;

    // Explosion
    protected bool isExploding;
    protected int comboIdx;
    protected int comboChain;

    //==========================Explosion=============================
    public void ExplodeByBomb()
    {
        if (this.isExploding)
            return;

        this.comboIdx = Game.StartCombo();
        this.comboChain = 1;

        Explode();
    }

    public void ExplodeByOthers()
    {
        if (this.isExploding)
            return;

        // TODO: 
    }

    public void TriggerExplode(Enemy enemy)
    {
        if (enemy.isExploding)
            return;

        enemy.comboIdx = this.comboIdx;
        enemy.comboChain = Game.ChainCombo(this.comboIdx);

        Explode();
    }

    protected void Explode()
    {
        this.isExploding = true;

        Player.AddScore(this.Points * this.comboChain);
        PlayExplodeAnim();
        ShowScoreText();
    }

    protected void PlayExplodeAnim()
    {
        // TODO:
    }

    protected void ShowScoreText()
    {
        // TODO:
    }

    //============================Collision================================
    public bool IsColliding(Enemy other)
    {
        if (this.Box.Overlaps(other.Box))
            return true;

        return false;
    }

    public bool IsColliding(Ship ship)
    {
        if (this.Box.Overlaps(ship.Box))
            return true;

        return false;
    }

    //==========================Shoot=======================================
    protected void GenerateNextShootTime()
    {
        var interval = Random.Range(this.ShootIntervalMin, this.ShootIntervalMax);
        this.nextShootTime = Time.time + interval;
    }

    //======================================================================
    public virtual void Start()
    {
        this.speed = Random.Range(SpeedMin, SpeedMax);
        this.initSpeed = this.speed;
        var spawnPos = Random.Range(SpawnFloor, SpawnCeiling);

        // Init for creep up movement
        if (Motion == Motions.CreepUp)
        {
            this.transform.position = new Vector3(spawnPos, Dimensions.BOT_EDGE, this.transform.position.z);
            return;
        }

        // Init for linear and accelerated movements
        var dirRandom = Random.Range(0, 2);
        if (dirRandom == 0)
        {
            this.dir = Vector3.right;
            this.transform.position = new Vector3(Dimensions.LEFT_EDGE, spawnPos, this.transform.position.z);
            return;
        }

        this.dir = Vector3.left;
        this.transform.position = new Vector3(Dimensions.RIGHT_EDGE, spawnPos, this.transform.position.z);

        GenerateNextShootTime();
    }

    public virtual void Update()
    {
        if (this.isExploding)
        {
            ExplosionUpdate();
            return;
        }

        CollisionUpdate();

        MoveUpdate();
        ShootUpdate();
    }

    protected void ExplosionUpdate()
    {
        Game.CheckExplosion(this);
    }

    protected void CollisionUpdate()
    {
        if (! IsColliding(Game.PlayerShip))
            return;

        Game.PlayerShip.Explode();
        this.Explode();
    }

    protected void MoveUpdate()
    {
        // Update for creep up movement
        if (Motion == Motions.CreepUp)
        {
            UpdateStateChange();
            DoCreepUpMovement();
            return;
        }

        // Update for linear and accelerated movements
        if (this.dir == Vector3.right &&
            this.transform.position.x > Dimensions.RIGHT_EDGE)
        {
            Destroy();
            return;
        }

        if (this.dir == Vector3.left &&
            this.transform.position.x < Dimensions.LEFT_EDGE)
        {
            Destroy();
            return;
        }

        // Update for accelerated movement
        if (Motion == Motions.Accelerated)
        {
            DoAcceleratedMovement();
            return;
        }

        // Update for linear movement
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    protected void ShootUpdate()
    {
        if (this.Weapon == null)
            return;

        if (Time.time < this.nextShootTime)
            return;

        // For interval shoot
        if (this.ShootStyle == ShootStyles.Interval)
        {
            Shoot();
            return;
        }

        // For in range shoot
        if (Mathf.Abs(this.transform.position.x - Game.PlayerShip.transform.position.x) > SHOOT_RANGE)
            return;

        Shoot();
    }

    protected const float PAUSE_INTERVAL = 2f;
    protected const float MOVE_INTERVAL = 1f;
    protected float lastChangeTime;
    protected bool isPaused;

    protected void UpdateStateChange()
    {
        if (this.isPaused && Time.time - this.lastChangeTime > PAUSE_INTERVAL)
        {
            this.isPaused = false;
            this.lastChangeTime = Time.time;

            this.dir = (Game.PlayerShip.transform.position - this.transform.position).normalized;

            return;
        }

        if (this.isPaused)
            return;

        if (Time.time - this.lastChangeTime < MOVE_INTERVAL)
            return;

        this.isPaused = true;
        this.lastChangeTime = Time.time;
    }

    protected void DoCreepUpMovement()
    {
        if (this.isPaused)
            return;

        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    protected const float DETECT_RANGE = 1.5f;
    protected void DoAcceleratedMovement()
    {
        var dist = Mathf.Abs(this.transform.position.x - Game.PlayerShip.transform.position.x);
        if (dist > DETECT_RANGE)
        {
            this.transform.position += this.dir * this.initSpeed * Time.deltaTime;
            return;
        }

        this.speed = dist / DETECT_RANGE * this.initSpeed;
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    protected const float SHOOT_RANGE = 0.05f;
    protected void Shoot()
    {
        var projectile = GameObject.Instantiate(Weapon) as GameObject;
        projectile.transform.position = this.transform.position;

        GenerateNextShootTime();
    }

    protected void Destroy()
    {
        Destroy(this.gameObject);
    }
}