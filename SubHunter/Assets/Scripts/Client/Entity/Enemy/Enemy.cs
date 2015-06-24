using UnityEngine;
using Foundation;

public class Enemy : Entity
{
    public float SpawnFloor;
    public float SpawnCeiling;
    public int   Points;

    protected bool isExploding;
    protected int  comboIdx;

    public bool IsExploding { get { return this.isExploding; } }
    public bool ComboIdx { get { return this.comboIdx; } }

    public void ExplodeByBomb()
    {
        if (this.isExploding)
            return;

        this.isExploding = true;

        InstantiateExplosion(Combo.I.StartCombo());

        InstantiateScore(this.Points, 1);
        Player.I.AddScore(this.Points);
    }

    public void ExplodeByExplosion(int comboIdx)
    {
        if (this.isExploding)
            return;

        this.isExploding = true;

        InstantiateExplosion(comboIdx);

        var multiplier = Combo.I.ChainCombo(comboIdx);
        Player.I.AddScore(this.Points * multiplier);
        InstantiateScore(this.Points, multiplier);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
    }

    private void InstantiateExplosion(int comboIdx)
    {
        var explosionGO = GameObject.Instantiate(Game.I.Spawner.Explosion, this.transform.position, Quaternion.identity) as GameObject;
        var explosion = explosionGO.GetComponent<Explosion>();
        explosion.SetCombIdx(comboIdx);

        EntityManager.I.Explosions.Add(explosion);
    }

    private void InstantiateScore(int score, int multiplier)
    {
        // TODO:
    }
}