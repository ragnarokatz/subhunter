using UnityEngine;
using Foundation;

public enum Movement
{
    Linear,
    Accelerated,
    CreepUp,
}

public enum FireType
{
    Interval,
    InRange,
}

public class Enemy : MonoBehaviour
{
    // Public properties
    public float      BoxWidth;
    public float      BoxHeight;
    public Movement   MoveType;
    public float      SpeedMin;
    public float      SpeedMax;
    public float      SpawnCeiling;
    public float      SpawnFloor;
    public GameObject Weapon;
    public float      FireInterval;
    public FireType   FireType;
    public float      Delay;

    // Movement
    private float initSpeed;
    private float speed;
    private Vector3 dir;

    // Fire
    private float lastFireTime;
    private bool isExploding;

    private const float LEFT_EDGE = -7f;
    private const float RIGHT_EDGE = 7f;
    private const float BOTTOM_EDGE = -4f;

    // Collision
    public Rect Box = new Rect(0f, 0f, BoxWidth * 2, BoxHeight * 2);

    void Start()
    {
        this.speed = Random.Range(SpeedMin, SpeedMax);
        this.initSpeed = this.speed;
        var spawnPos = Random.Range(SpawnFloor, SpawnCeiling);

        // Init for creep up movement
        if (MoveType == Movement.CreepUp)
        {
            this.transform.position = new Vector3(spawnPos, BOTTOM_EDGE, this.transform.position.z);
            return;
        }

        // Init for linear and accelerated movements
        var dirRandom = Random.Range(0, 2);
        if (dirRandom == 0)
        {
            this.dir = Vector3.right;
            this.transform.position = new Vector3(LEFT_EDGE, spawnPos, this.transform.position.z);
            return;
        }

        this.dir = Vector3.left;
        this.transform.position = new Vector3(RIGHT_EDGE, spawnPos, this.transform.position.z);

        this.lastFireTime = Time.time;
    }

    void Update()
    {
        UpdateBox();
        UpdateMove();
        UpdateFire();
    }

    public void Explode()
    {
        if (this.isExploding)
            return;

        this.isExploding = true;
    }

    private void UpdateBox()
    {
        this.Box.Set(this.transform.position.x - this.BoxWidth, this.transform.position.y - this.BoxHeight, this.BoxWidth, this.BoxHeight);

        var ship = Init.I.PlayerShip.GetComponent<Ship>();
        if (! this.Box.Overlaps(ship.Box))
            return;

        ship.Destroy();
    }

    private void UpdateMove()
    {
        // Update for creep up movement
        if (MoveType == Movement.CreepUp)
        {
            UpdateStateChange();
            DoCreepUpMovement();
            return;
        }

        // Update for linear and accelerated movements
        if (this.dir == Vector3.right &&
            this.transform.position.x > RIGHT_EDGE)
        {
            Destroy();
            return;
        }

        if (this.dir == Vector3.left &&
            this.transform.position.x < LEFT_EDGE)
        {
            Destroy();
            return;
        }

        // Update for accelerated movement
        if (MoveType == Movement.Accelerated)
        {
            DoAcceleratedMovement();
            return;
        }

        // Update for linear movement
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private void UpdateFire()
    {
        if (this.Weapon == null)
            return;

        if (Time.time - this.lastFireTime < FireInterval)
            return;

        // For interval fire
        if (this.FireType == FireType.Interval)
        {
            Fire();
            return;
        }

        // For in range fire
        if (Mathf.Abs(this.transform.position.x - Init.I.PlayerShip.transform.position.x) > FIRE_RANGE)
            return;

        Fire();
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

            this.dir = (Init.I.PlayerShip.transform.position - this.transform.position).normalized;

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
        var dist = Mathf.Abs(this.transform.position.x - Init.I.PlayerShip.transform.position.x);
        if (dist > DETECT_RANGE)
        {
            this.transform.position += this.dir * this.initSpeed * Time.deltaTime;
            return;
        }

        this.speed = dist / DETECT_RANGE * this.initSpeed;
        this.transform.position += this.dir * this.speed * Time.deltaTime;
    }

    private const float FIRE_RANGE = 0.05f;
    private void Fire()
    {
        var projectile = GameObject.Instantiate(Weapon) as GameObject;
        projectile.transform.position = this.transform.position;

        this.lastFireTime = Time.time;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}