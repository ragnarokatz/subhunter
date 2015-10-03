using System;
using UnityEngine;

public class ExplosionAutoEnd : MonoBehaviour
{
    public float Duration;
    public AudioSource source;

    private float startTime;

    private void Start()
    {
        this.startTime = Time.time;
        this.source.Play();
        this.transform.SetParent(EntityManager.I.ExplosionParent, true);
    }

    private void Update()
    {
        if (Time.time - this.startTime < this.Duration)
            return;

        GameObject.Destroy(this.gameObject);
    }
}