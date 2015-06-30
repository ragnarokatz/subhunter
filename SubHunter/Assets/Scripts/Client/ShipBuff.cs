using UnityEngine;
using Foundation;

public class ShipBuff : MonoBehaviour
{
    public Sprite Normal;
    public Sprite Speedup;
    public Sprite Invul;

    private bool           isFlashing;
    private Animator       anim;
    private SpriteRenderer renderer;

    private void Awake()
    {
        EventManager.OnUpdateBuff += UpdateShipSprite;

        this.anim = GetComponent<Animator>();
        this.renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (! BuffManager.I.IsInBuff)
            return;

        if (BuffManager.I.TimeLeft > 3f)
            return;

        if (this.isFlashing)
            return;

        this.isFlashing = true;

        if (BuffManager.I.IsInInvulState())
            this.anim.SetTrigger("Invul");
        else if (BuffManager.I.IsInSpeedupState())
            this.anim.SetTrigger("Speedup");

        this.Invoke("RestoreToNormalSprite", 3f);
    }
    
    private void UpdateShipSprite (object type)
    {
        if (this.IsInvoking("RestoreToNormalSprite"))
            this.CancelInvoke("RestoreToNormalSprite");

        this.isFlashing = false;

        if (BuffManager.I.IsInInvulState())
        {
            this.renderer.sprite = this.Invul;
            return;
        }
        
        if (BuffManager.I.IsInSpeedupState())
        {
            this.renderer.sprite = this.Speedup;
            return;
        }
    }

    private void RestoreToNormalSprite()
    {
        this.renderer.sprite = this.Normal;
    }
}
