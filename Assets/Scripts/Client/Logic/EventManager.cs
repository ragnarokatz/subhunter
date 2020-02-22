using System;

public class EventManager
{
    public delegate void UpdateHandler(object[] para);
    public static event UpdateHandler OnUpdateAttribs;
    public static event UpdateHandler OnUpdateBuff;

    public static void UpdateAttribs(string type, bool playAnim, string animType = null)
    {
        if (EventManager.OnUpdateAttribs != null)
            EventManager.OnUpdateAttribs(new object[] { type, playAnim, animType });
    }

    public static void UpdateBuff()
    {
        if (EventManager.OnUpdateBuff != null)
            EventManager.OnUpdateBuff(null);
    }
}