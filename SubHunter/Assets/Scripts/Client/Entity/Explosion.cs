using UnityEngine;
using System.IO;
using Foundation;

public class Explosion : Entity
{
    public float Duration;

    private float startTime;
    private int   comboIdx;

    public int ComboIdx { get { return this.comboIdx; } }

    public void SetCombIdx(int comboIdx)
    {
        this.comboIdx = comboIdx;
    }

    private void Start()
    {
        this.startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - this.startTime < this.Duration)
            return;

        this.Destroy();
        EntityManager.I.Explosions.Remove(this);
    }
}
