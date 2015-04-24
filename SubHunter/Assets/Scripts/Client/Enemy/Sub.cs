using UnityEngine;

public class Sub : Enemy
{
    public float SpeedMin;
    public float SpeedMax;

    public float ShootIntervalMin;
    public float ShootIntervalMax;

    private float shootInterval;

    public virtual void Start ()
    {
        this.shootInterval = Random.Range(this.ShootIntervalMin, this.ShootIntervalMax);
    }

    public virtual void Update ()
    {
    }
}