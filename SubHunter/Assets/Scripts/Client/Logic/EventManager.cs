using System;

public class EventManager
{
    public delegate void UpdateHandler(object type);
    public static event UpdateHandler OnUpdateAttrib;
    public static event UpdateHandler OnUpdateBuff;

    public static void UpdateAttrib(object type)
    {
        if (EventManager.OnUpdateAttrib != null)
            EventManager.OnUpdateAttrib(type);
    }

    public static void UpdateBuff()
    {
        if (EventManager.OnUpdateBuff != null)
            EventManager.OnUpdateBuff(null);
    }
}