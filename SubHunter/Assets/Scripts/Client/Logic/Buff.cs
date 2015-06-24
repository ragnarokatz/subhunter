using UnityEngine;
using Foundation;

namespace Buff
{
    public class Buff
    {
        public static Invul   InvulBuff = new Invul();
        public static Speedup SpeedupBuff = new Speedup();

        protected float duration;
    }

    public class Invul : Buff
    {
    }

    public class Speedup : Buff
    {
    }
}
