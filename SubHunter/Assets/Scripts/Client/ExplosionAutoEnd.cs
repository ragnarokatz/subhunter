using System;
using UnityEngine;

public class ExplosionAutoEnd : MonoBehaviour
{
    public float Duration;

    private float startTime;

    private void Start()
    {
        this.startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - this.startTime < this.Duration)
            return;

        GameObject.Destroy(this.gameObject);
    }
}