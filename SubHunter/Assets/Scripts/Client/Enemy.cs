using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float SpeedMin;
    public float SpeedMax;

    public float SpawnCeiling;
    public float SpawnFloor;

    public GameObject Weapon;
    public float FireInterval;

    private float velocity;
    private float acceleration;

    private Vector3 dir;

    private const float LEFT_EDGE = -7f;
    private const float RIGHT_EDGE = 7f;

    private float lastFireTime;

    void Start()
    {
        this.velocity = Random.Range(SpeedMin, SpeedMax);
        var height = Random.Range(SpawnFloor, SpawnCeiling);

        var dirRandom = Random.Range(0, 2);
        if (dirRandom == 0)
        {
            this.dir = Vector3.right;
            this.transform.position = new Vector3(LEFT_EDGE, height, this.transform.position.z);
            return;
        }

        this.dir = Vector3.left;
        this.transform.position = new Vector3(RIGHT_EDGE, height, this.transform.position.z);

        this.lastFireTime = Time.time;
    }

    void Update()
    {
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

        this.transform.position += this.dir * this.velocity;

        if (this.Weapon == null)
            return;

        if (Time.time - this.lastFireTime < FireInterval)
            return;

        Fire();
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