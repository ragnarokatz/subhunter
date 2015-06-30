using System;

public class EventManager
{
    public delegate void UpdateHandler(string type);
    public static event UpdateHandler OnUpdate;

    public static void UpdateAttrib(string type)
    {
        if (EventManager.OnUpdate != null)
            EventManager.OnUpdate(type);
    }
}