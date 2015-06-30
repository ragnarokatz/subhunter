using System;

public class EventManager
{
    public delegate void UpdateHandler(object type);
    public static event UpdateHandler OnUpdate;

    public static void UpdateAttrib(object type)
    {
        if (EventManager.OnUpdate != null)
            EventManager.OnUpdate(type);
    }
}