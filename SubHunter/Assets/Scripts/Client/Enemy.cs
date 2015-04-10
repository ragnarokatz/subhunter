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
    // Public properties
    public float BoxWidth;
    public float BoxHeight;

    public Motions Motion;

    public float SpeedMin;
    public float SpeedMax;

    public float SpawnCeiling;
    public float SpawnFloor;

    public GameObject Weapon;

    public ShootStyles ShootStyle;
    public float ShootIntervalMin;
    public float ShootIntervalMax;

    public int Points;

    // Collision
    private Rect box { get { return new Rect(this.transform.position.x - this.BoxWidth / 2,
    this.transform.position.y - this.BoxHeight / 2, this.BoxWidth, this.BoxHeight); } }

    // Movement
    private float initSpeed;
    private float speed;
    private Vector3 dir;

    // Shoot
    private float nextShootTime;

    // Explosion
    private bool isExploding;
    private int comboIdx;
    private int comboChain;

    //==========================Explosion=============================
    public void ExplodeByBomb()
    {
        if (this.isExploding)
            return;

        this.comboIdx = Game.StartCombo();
        this.comboChain = 1;

        Explode();
    }

    public void TriggerExplode(Enemy enemy)
    {
        if (enemy.isExploding)
            return;

        enemy.comboIdx = this.comboIdx;
        enemy.comboChain = Game.ChainCombo(this.comboIdx);

        Explode();
    }

    private void Explode()
    {
        this.isExploding = true;

        Player.AddScore(this.Points * this.comboChain);
        PlayExplodeAnim();
        ShowScoreText();
    }

    private void PlayExplodeAnim()
    {
        // TODO:
    }

    private void ShowScoreText()
    {
    }

    //============================Collision================================
    public bool IsColliding(Enemy other)
    {
        if (this.box.Overlaps(other.box))
            return true;

        return false;
    }

    public bool IsColliding(Ship ship)
    {
        if (this.box.Overlaps(ship.Box))
            return true;

        return false;
    }

    //==========================Shoot=======================================
    private void GenerateNextShootTime()
    {
        var interval = Random.Range(this.ShootIntervalMin, this.ShootIntervalMax);
        this.nextShootTime = Time.time + interval;
    }

    //======================================================================
    void Start()
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

    void Update()
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

    private void ExplosionUpdate()
    {
        Game.CheckExplosion(this);
    }

    private void CollisionUpdate()
    {
        if (! IsColliding(Game.PlayerShip))
            return;

        Game.PlayerShip.Explode();
        this.Explode();
    }

    private void MoveUpdate()
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

    private void ShootUpdate()
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

    private const float PAUSE_INTERVAL = 2f;
    private const float MOVE_INTERVAL = 1f;
    private float lastChangeTime;
    private bool isPaused;

    private void UpdateStateChange()
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

    private void DoCreepUpMovement()
    {
        if (this.isPaused)
            return;

        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private const float DETECT_RANGE = 1.5f;
    private void DoAcceleratedMovement()
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

    private const float SHOOT_RANGE = 0.05f;
    private void Shoot()
    {
        var projectile = GameObject.Instantiate(Weapon) as GameObject;
        projectile.transform.position = this.transform.position;

        GenerateNextShootTime();
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}