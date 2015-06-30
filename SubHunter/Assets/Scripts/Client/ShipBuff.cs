using UnityEngine;
using Foundation;

public class ShipBuff : MonoBehaviour
{
    public Sprite Normal;
    public Sprite Speedup;
    public Sprite Invul;

    private Animator anim;
    private SpriteRenderer renderer;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
        this.renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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

        this.renderer.sprite = this.Normal;
    }
}
