using UnityEngine;
using Foundation;

namespace Buff
{
    public class BuffManager : MonoBehaviour
    {
        private class Buff
        {
            protected float duration;
            public float Duration { get { return this.duration; } }

            public Buff(float duration)
            {
                this.duration = duration;
            }

            public virtual void StartEffect()
            {
            }

            public virtual void EndEffect()
            {
            }
        }
        
        private class Invul : Buff
        {
            public Invul(float duration) : base(duration)
            {
            }
        }
        
        private class Speedup : Buff
        {
            public Speedup(float duration) : base(duration)
            {
            }

            public override void StartEffect()
            {
                Game.I.Ship.Speedup();
            }
            
            public override void EndEffect()
            {
                Game.I.Ship.RestoreSpeed();
            }
        }

        private static BuffManager instance;

        private static Invul   StartBuff   = new Invul(3f);
        private static Invul   InvulBuff   = new Invul(15f);
        private static Speedup SpeedupBuff = new Speedup(15f);

        private static Buff current;

        public static void AddStartBuff()
        {
            AddBuff(BuffManager.InvulBuff);
        }

        public static void AddInvulBuff()
        {
            AddBuff(BuffManager.InvulBuff);
        }

        public static void AddSpeedupBuff()
        {
            AddBuff(BuffManager.SpeedupBuff);
        }

        private static void AddBuff(Buff buff)
        {
            if (BuffManager.current != null)
                BuffManager.current.EndEffect();

            BuffManager.current = buff;
            BuffManager.current.StartEffect();

            BuffManager.instance.isInBuff = true;
            BuffManager.instance.startTime = Time.time;
        }

        private bool  isInBuff;
        private float startTime;

        public float TimeLeft { get { return BuffManager.current.Duration - (Time.time - this.startTime); } }

        private void Start()
        {
            BuffManager.instance = this;
        }

        private void Update()
        {
            if (! this.isInBuff)
                return;

            System.Diagnostics.Debug.Assert(BuffManager.current != null);

            if (this.TimeLeft > 0f)
                return;

            this.isInBuff = false;
            BuffManager.current.EndEffect();
            BuffManager.current = null;
        }
    }
}
