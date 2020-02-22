using UnityEngine;
using Foundation;

public class ShipBuff : MonoBehaviour
{
    private bool     isFlashing;
    private Animator anim;

    private void Awake()
    {
        EventManager.OnUpdateBuff += UpdateShipSprite;

        this.anim = GetComponent<Animator>();
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
            this.anim.Play ("invulflash");
        else if (BuffManager.I.IsInSpeedupState())
            this.anim.Play ("speedupflash");
    }
    
    private void UpdateShipSprite (object type)
    {
        Log.Trace("Update ship sprite.");

        this.isFlashing = false;

        if (BuffManager.I.IsInInvulState())
        {
            this.anim.Play ("invul");
            return;
        }
        
        if (BuffManager.I.IsInSpeedupState())
        {
            this.anim.Play ("speedup");
            return;
        }

        this.anim.Play ("normal");
    }
    
    private void OnDestroy()
    {
        EventManager.OnUpdateBuff -= UpdateShipSprite;
    }
}
