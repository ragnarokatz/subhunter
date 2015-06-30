using UnityEngine;
using Foundation;

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
            Log.Trace("Start buff {0}", this.GetType());
        }

        public virtual void EndEffect()
        {
            Log.Trace("End buff {0}", this.GetType());
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
            Ship.Data.Speedup();

            base.StartEffect();
        }
        
        public override void EndEffect()
        {
            Ship.Data.RestoreSpeed();

            base.EndEffect();
        }
    }

    private static BuffManager instance;
    public static BuffManager I { get { return BuffManager.instance; } }

    private static Invul   StartBuff   = new Invul(3f);
    private static Invul   InvulBuff   = new Invul(15f);
    private static Speedup SpeedupBuff = new Speedup(15f);

    private Buff  current;
    private bool  isInBuff;
    private float startTime;

    public float TimeLeft { get { return this.current.Duration - (Time.time - this.startTime); } }

    public void AddStartBuff()
    {
        AddBuff(BuffManager.StartBuff);
    }

    public void AddInvulBuff()
    {
        AddBuff(BuffManager.InvulBuff);
    }

    public void AddSpeedupBuff()
    {
        AddBuff(BuffManager.SpeedupBuff);
    }

    public bool IsInInvulState()
    {
        if (! this.isInBuff)
            return false;

        return this.current.GetType() == typeof(Invul);
    }

    public bool IsInSpeedupState()
    {
        if (! this.isInBuff)
            return false;

        return this.current.GetType() == typeof(Speedup);
    }

    private void AddBuff(Buff buff)
    {
        if (this.current != null)
            this.current.EndEffect();

        this.current = buff;
        this.current.StartEffect();

        this.isInBuff = true;
        this.startTime = Time.time;
    }

    private void Start()
    {
        Log.Assert(BuffManager.instance == null);

        BuffManager.instance = this;
        this.isInBuff = false;
    }

    private void Update()
    {
        if (! this.isInBuff)
            return;

        Log.Assert(this.current != null);

        if (this.TimeLeft > 0f)
            return;

        this.isInBuff = false;
        this.current.EndEffect();
        this.current = null;
    }
}
