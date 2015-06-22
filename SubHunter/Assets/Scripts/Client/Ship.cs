using UnityEngine;
using System.IO;
using Foundation;

public class Ship : MonoBehaviour
{
    public float Width;
    public float Height;

    public float Speed;
    public float FireInterval;

    private float lastFireLeftTime;
    private float lastFireRightTime;
    private float lastFireMiddleTime;

    public Rect Box
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

    public void Explode()
    {
        Explosion.StartExplosion(this.transform.position);
    }

    void Update()
    {
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

    public void MoveLeft()
    {
        if (this.transform.position.x <= Dimensions.LEFT_EDGE)
            return;

        this.transform.position += Vector3.left * Speed * Time.deltaTime;
    }

    public void MoveRight()
    {
        if (this.transform.position.x >= Dimensions.RIGHT_EDGE)
            return;

        this.transform.position += Vector3.right * Speed * Time.deltaTime;
    }

    public void FireLeft()
    {
        if (Time.time - this.lastFireLeftTime < this.FireInterval)
            return;

        Log.Trace("Firing left.");
        this.lastFireLeftTime = Time.time;
    }

    public void FireRight()
    {
        if (Time.time - this.lastFireRightTime < this.FireInterval)
            return;

        Log.Trace("Firing right.");
        this.lastFireRightTime = Time.time;
    }

    public void FireMiddle()
    {
        if (Time.time - this.lastFireMiddleTime < this.FireInterval)
            return;

        Log.Trace("Firing middle.");
        this.lastFireMiddleTime = Time.time;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
