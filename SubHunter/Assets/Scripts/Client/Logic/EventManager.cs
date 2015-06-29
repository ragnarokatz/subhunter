using System;

public class EventManager
{
    public delegate void UpdateHandler(string type, int value);
    public static event UpdateHandler OnUpdate;

    public static void UpdateAttrib(string type, int value)
    {
        if (EventManager.OnUpdate != null)
            EventManager.OnUpdate(type, value);
    }
}