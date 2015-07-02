using System;

public class EventManager
{
    public delegate void UpdateHandler(object type);
    public static event UpdateHandler OnUpdateAttribs;
    public static event UpdateHandler OnUpdateBuff;

    public static void UpdateAttribs(object type)
    {
        if (EventManager.OnUpdateAttribs != null)
            EventManager.OnUpdateAttribs(type);
    }

    public static void UpdateBuff()
    {
        if (EventManager.OnUpdateBuff != null)
            EventManager.OnUpdateBuff(null);
    }
}