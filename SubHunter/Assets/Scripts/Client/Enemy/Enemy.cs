using UnityEngine;
using Foundation;

public class Enemy : MonoBehaviour
{
    // Public properties
    public float Width;
    public float Height;
    public float SpeedMin;
    public float SpeedMax;
    public float SpawnCeiling;
    public float SpawnFloor;
    public int   Points;

    protected Vector3 dir;
    protected float   speed;
    protected bool    isExploding;

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
        if (this.isExploding)
            return;

        this.isExploding = true;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        this.speed = Random.Range(SpeedMin, SpeedMax);
    }

    protected virtual void Update()
    {
    }
}