using UnityEngine;
using UnityEngine.UI;
using Foundation;
using System;

public class Enemy : Entity
{
    public float SpawnFloor;
    public float SpawnCeiling;
    public int   Points;

    protected bool  isExploding;
    protected int   comboIdx;

    private float explodeDuration;
    private float explodeStartTime;

    public bool IsExploding { get { return this.isExploding; } }
    public int  ComboIdx    { get { return this.comboIdx; } }

    public void Explode(int comboIdx = -1)
    {
        if (this.isExploding)
            return;

        this.isExploding = true;
        this.explodeStartTime = Time.time;
        var multiplier = 1;

        if (comboIdx == -1)
        {
            this.comboIdx = Combo.I.StartCombo();
            Player.I.AddScore(this.Points);
        } else
        {
            multiplier = Combo.I.ChainCombo(comboIdx);
            Player.I.AddScore(this.Points * multiplier);
        }
        
        GameObject.Instantiate(Game.I.Spawner.Explosion, this.transform.position, Quaternion.identity);
        var scoreGO = GameObject.Instantiate(Game.I.Spawner.Score, this.transform.position, Quaternion.identity) as GameObject;
        scoreGO.GetComponent<Text>().text = String.Format("{0} X {1}", this.Points, multiplier);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (! this.isExploding)
        {
            base.Update();
            return;
        }

        if (Time.time - this.explodeStartTime < this.explodeDuration)
            return;

        Destroy ();
    }

    // TODO: ////----
    #if false
    private void AlignScoreToEnemy()
    {
        var canvasRect = this.mainCanvas.GetComponent<RectTransform>();
        var bfCamera = this.battlefieldCamera.gameObject.GetComponent<Camera>();
        var uiMount = character.FindChild("UIMount");
        var viewportPos = bfCamera.WorldToViewportPoint(uiMount.transform.position);
        var screenPos = new Vector2(
            ((viewportPos.x * canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y * 0.5f)));
        
        var rect = hud.GetComponent<RectTransform>();
        rect.anchoredPosition = screenPos;
    }
    #endif
}