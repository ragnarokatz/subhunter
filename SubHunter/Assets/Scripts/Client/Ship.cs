using UnityEngine;
using System.IO;
using Foundation;

public class Ship : MonoBehaviour
{
    public float BoxWidth;
    public float BoxHeight;

    private const float START_X = 0f;
    private const float SPEED = 3f;

    private const float LEFT_EDGE = -6.5f;
    private const float RIGHT_EDGE = 6.5f;

    private const float FIRE_INTERVAL = 1f;

    private float lastFireLeftTime;
    private float lastFireRightTime;
    private float lastFireMiddleTime;

    public Rect Box = new Rect(0f, 0f, this.BoxWidth, this.BoxHeight);

    void Start()
    {
        this.transform.position = new Vector3(START_X, this.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        UpdateBox();

        if (Input.GetKey(KeyCode.Z))
            FireLeft();

        if (Input.GetKey(KeyCode.X))
            FireRight();

        if (Input.GetKey(KeyCode.Space))
            FireMiddle();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
            return;
        }
    }

    private void UpdateBox()
    {
        this.Box.Set(this.transform.position.x - this.BoxWidth, this.transform.position.y - this.BoxHeight, this.BoxWidth, this.BoxHeight);
    }

    public void MoveLeft()
    {
        if (this.transform.position.x <= LEFT_EDGE)
            return;

        this.transform.position += Vector3.left * SPEED * Time.deltaTime;
    }

    public void MoveRight()
    {
        if (this.transform.position.x >= RIGHT_EDGE)
            return;

        this.transform.position += Vector3.right * SPEED * Time.deltaTime;
    }

    public void FireLeft()
    {
        if (Time.time - this.lastFireLeftTime < FIRE_INTERVAL)
            return;

        Log.Trace("Firing left.");
        this.lastFireLeftTime = Time.time;
    }

    public void FireRight()
    {
        if (Time.time - this.lastFireRightTime < FIRE_INTERVAL)
            return;

        Log.Trace("Firing right.");
        this.lastFireRightTime = Time.time;
    }

    public void FireMiddle()
    {
        if (Time.time - this.lastFireMiddleTime < FIRE_INTERVAL)
            return;

        Log.Trace("Firing middle.");
        this.lastFireMiddleTime = Time.time;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
