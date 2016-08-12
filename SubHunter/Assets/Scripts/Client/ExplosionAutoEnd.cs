using System;
using UnityEngine;

public class ExplosionAutoEnd : MonoBehaviour
{
    public float Duration;

    private float startTime;

    private void Start()
    {
        this.startTime = Time.time;
        AudioManager.I.AudioSources[1].Play();
        this.transform.SetParent(EntityManager.I.ExplosionParent, true);
    }

    private void Update()
    {
        if (Time.time - this.startTime < this.Duration)
            return;

        GameObject.Destroy(this.gameObject);
    }
}