using UnityEngine;

namespace SubHunter.Powerup
{
    public class Powerup : Entity
    {
        protected float destroyBoundary;
        protected float delayDuration;
        protected float delayStartTime;
        protected bool  isInDelay;

        protected override void Start()
        {
            base.Start();

            this.dir = Vector3.up;
            this.destroyBoundary = Dimensions.WATER;

            EntityManager.I.Powerups.Add(this);
            this.transform.SetParent(EntityManager.I.PowerupParent, true);
        }

        protected override void Update()
        {
            base.Update();

            if (this.isInDelay)
            {
                if (Time.time - this.delayStartTime < this.delayDuration)
                    return;
                
                Destroy();
                return;
            }

            if (this.transform.position.y > this.destroyBoundary)
            {
                this.isInDelay = true;
                this.delayStartTime = Time.time;
                this.speed = 0f;
                this.dir = Vector3.zero;
                return;
            }

            this.transform.position += this.dir * this.speed * Time.deltaTime;
        }

        public override void Destroy ()
        {
            base.Destroy ();

            EntityManager.I.Powerups.Remove(this);
        }

        public virtual void Effect()
        {
        }
    }

    public class ExtraLife : Powerup
    {
        public override void Effect()
        {
            Player.I.GainAnExtraLife();
        }
    }

    public class ExtraClip : Powerup
    {
        public override void Effect()
        {
            Player.I.GainAnExtraClip();
        }
    }

    public class StopTime : Powerup
    {
        public override void Effect()
        {

        }
    }

    public class BonusPoints : Powerup
    {
        public override void Effect()
        {
            Player.I.AddScore(2000);
        }
    }

    public class Nuke : Powerup
    {
        public override void Effect()
        {
            Ship.Data.Nukes++;
        }
    }

    public class Speedup : Powerup
    {
        public override void Effect()
        {
            BuffManager.I.AddSpeedupBuff();
        }
    }

    public class Invulnerability : Powerup
    {
        public override void Effect()
        {
            BuffManager.I.AddInvulBuff();
        }
    }
}