using UnityEngine;
using Foundation;

public class Entity : MonoBehaviour
{
    public float Width;
    public float Height;
    public float SpeedMin;
    public float SpeedMax;

    protected Vector3 dir;
    protected float   speed;

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

    public virtual void Destroy()
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