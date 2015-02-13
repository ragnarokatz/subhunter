using UnityEngine;
using Foundation;

public enum Movement
{
    Linear,
    Accelerated,
    CreepUp,
}

public class Enemy : MonoBehaviour
{
    public Movement MoveType;

    public float SpeedMin;
    public float SpeedMax;

    public float SpawnCeiling;
    public float SpawnFloor;

    public GameObject Weapon;
    public float FireInterval;

    private float initSpeed;
    private float speed;
    private float acceleration;

    private Vector3 dir;

    private const float LEFT_EDGE = -7f;
    private const float RIGHT_EDGE = 7f;
    private const float BOTTOM_EDGE = -4f;

    private float lastFireTime;

    void Start()
    {
        this.speed = Random.Range(SpeedMin, SpeedMax);
        this.initSpeed = this.speed;
        var spawnPos = Random.Range(SpawnFloor, SpawnCeiling);

        if (MoveType == Movement.CreepUp)
        {
            this.transform.position = new Vector3(spawnPos, BOTTOM_EDGE, this.transform.position.z);
            return;
        }

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
        if (MoveType == Movement.CreepUp)
        {
            CheckStateChange();
            DoCreepUpMovement();
            return;
        }

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

        if (MoveType == Movement.Accelerated)
            DoAcceleratedMovement();

        this.transform.position += this.dir * this.speed;

        if (this.Weapon == null)
            return;

        if (Time.time - this.lastFireTime < FireInterval)
            return;

        if (MoveType == Movement.Accelerated &&
            Mathf.Abs(this.transform.position.x - Init.I.PlayerShip.transform.position.x) > DETECT_RANGE)
            return;

        Fire();
    }

    private const float DETECT_RANGE = 0.1f;

    private const float PAUSE_INTERVAL = 2f;
    private const float MOVE_INTERVAL = 1f;
    private float lastChangeTime;
    private bool isPaused;

    private void CheckStateChange()
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

        this.transform.position += this.dir * this.speed;
    }

    private const float TARGET_SPEED = 0.001f;
    private void DoAcceleratedMovement()
    {
        if ((this.dir == Vector3.left && Init.I.PlayerShip.transform.position.x < this.transform.position.x) ||
            (this.dir == Vector3.right && Init.I.PlayerShip.transform.position.x > this.transform.position.x))
        {
            var distance = Mathf.Abs(Init.I.PlayerShip.transform.position.x - this.transform.position.x);
            var avgSpeed = (this.speed - TARGET_SPEED) / 2;
            var reachTime = distance / avgSpeed;
            this.acceleration = (TARGET_SPEED - this.speed) / reachTime;
            this.speed += this.acceleration;
            if (this.speed < TARGET_SPEED) this.speed = TARGET_SPEED;
            return;
        }

        float dist;
        if (this.dir == Vector3.left)
            dist = this.transform.position.x - LEFT_EDGE;
        else
            dist = RIGHT_EDGE - this.transform.position.x;

        var rt = dist / ((this.initSpeed - this.speed) / 2);
        this.acceleration = (this.initSpeed - this.speed) / rt;
        this.speed += this.acceleration;
    }

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