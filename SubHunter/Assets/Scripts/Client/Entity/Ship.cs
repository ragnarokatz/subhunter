using UnityEngine;
using System.IO;
using Foundation;

public class Ship : Entity
{
    public float FireInterval;

    private float lastFireLeftTime;
    private float lastFireRightTime;
    private float lastFireMiddleTime;
    private bool  isExploding;
    private int   clips;
    private int   nukes;

    public void Explode()
    {
        if (this.isExploding)
            return;

        this.isExploding = true;

        GameObject.Instantiate(Game.I.Spawner.Explosion, this.transform.position, Quaternion.identity);
        Destroy ();
    }

    // Called only by 1. when bombs have been destroyed 2. when player picks up an extra clip
    public void AddClip()
    {
        this.clips++;
    }

    public void AddNuke()
    {
        this.nukes++;
    }

    public void Speedup()
    {
        this.speed = 10f;
    }

    public void RestoreSpeed()
    {
        this.speed = UnityEngine.Random(this.SpeedMin, this.SpeedMax);
    }

    public void MoveLeft()
    {
        if (this.transform.position.x <= Dimensions.LEFT_EDGE)
            return;

        this.transform.position += Vector3.left * this.speed * Time.deltaTime;
    }

    public void MoveRight()
    {
        if (this.transform.position.x >= Dimensions.RIGHT_EDGE)
            return;

        this.transform.position += Vector3.right * this.speed * Time.deltaTime;
    }

    public void FireLeft()
    {
        if (this.clips <= 0)
            return;

        if (Time.time - this.lastFireLeftTime < this.FireInterval)
            return;

        this.clips--;
        this.lastFireLeftTime = Time.time;
    }

    public void FireRight()
    {
        if (this.clips <= 0)
            return;

        if (Time.time - this.lastFireRightTime < this.FireInterval)
            return;

        this.clips--;
        this.lastFireRightTime = Time.time;
    }

    public void FireMiddle()
    {
        if (this.clips <= 0)
            return;

        if (Time.time - this.lastFireMiddleTime < this.FireInterval)
            return;

        this.clips--;
        this.lastFireMiddleTime = Time.time;
    }

    protected override void Start ()
    {
        base.Start ();

        this.transform.position = new Vector3(0f, Dimensions.WATER, 0f);
        this.clips = Player.I.MaxClip;
    }

    protected override void Update()
    {
        if (this.isExploding)
            return;

        if (Input.GetKey(KeyCode.Z))
            FireLeft();

        if (Input.GetKey(KeyCode.X))
            FireRight();

        if (Input.GetKey(KeyCode.Space))
            FireMiddle();

        if (Input.GetKey(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKey(KeyCode.RightArrow))
            MoveRight();
    }
}
