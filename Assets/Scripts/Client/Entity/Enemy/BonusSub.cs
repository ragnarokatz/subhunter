using UnityEngine;

public class BonusSub : Sub
{
    public override void Explode (int comboIdx)
    {
        if (this.isExploding)
            return;

        var powerup = Prefabs.GetRandomPowerup();
        GameObject.Instantiate(powerup, this.transform.position, Quaternion.identity);

        base.Explode (comboIdx);
    }
}