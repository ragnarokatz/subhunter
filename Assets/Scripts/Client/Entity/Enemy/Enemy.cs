using UnityEngine;
using UnityEngine.UI;
using Foundation;
using System;

public class Enemy : Entity
{
    protected const float EXPLODE_DURATION = 1f;

    public float SpawnFloor;
    public float SpawnCeiling;
    public int   Points;

    protected float explodeStartTime;
    protected bool  isExploding;
    protected int   comboIdx;

    public bool IsExploding { get { return this.isExploding; } }
    public int  ComboIdx    { get { return this.comboIdx; } }

    public void StopMovement()
    {
        this.speed = 0f;
    }

    public void ResumeMovement()
    {
        this.speed = UnityEngine.Random.Range(this.SpeedMin, this.SpeedMax);
    }

    public virtual void Explode(int comboIdx = -1)
    {
        if (this.isExploding)
            return;

        this.isExploding = true;
        this.explodeStartTime = Time.time;

        if (comboIdx == -1)
            this.comboIdx = Combo.StartCombo();
        else
            this.comboIdx = comboIdx;

        var multiplier = Combo.ChainCombo(this.comboIdx);
        Player.I.AddScore(this.Points * multiplier);

        // Graphic changes:
        // Instantiate explosion, score, and disable sprite/animation
        GameObject.Instantiate(Prefabs.Explosion, this.transform.position, Quaternion.identity);
        HUDControls.I.InstantiateScoreHUD(this.Points, multiplier, this.transform.position);

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.enabled = false;

        var renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }

    public override void Destroy ()
    {
        base.Destroy ();

        EntityManager.I.Enemies.Remove(this);
    }

    protected override void Start()
    {
        base.Start();

        EntityManager.I.Enemies.Add(this);
        this.transform.SetParent(EntityManager.I.EnemyParent, true);
    }

    protected override void Update()
    {
        if (! this.isExploding)
        {
            base.Update();
            return;
        }

        if (Time.time - this.explodeStartTime < Enemy.EXPLODE_DURATION)
            return;

        Destroy ();
    }
}