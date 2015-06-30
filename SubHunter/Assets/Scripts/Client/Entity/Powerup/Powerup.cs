using UnityEngine;

namespace SubHunter.Powerup
{
    public class Powerup : Entity
    {
        public float DelayDuration;

        protected float destroyBoundary;
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
                if (Time.time - this.delayStartTime < this.DelayDuration)
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
}