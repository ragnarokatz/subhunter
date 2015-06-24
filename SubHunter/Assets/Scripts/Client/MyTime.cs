using UnityEngine;

public class MyTime : MonoBehaviour
{
    private static bool  isWorking;
    private static float t;
    private static float dt;

    public static float time { get { return t; } }
    public static float deltaTime { get { return dt; } }

    public static void StartTime()
    {
        MyTime.t = Time.time;
    }

    public static void PauseTime(float duration)
    {
        MyTime.isWorking = false;
        Invoke("ResumeTime", duration);
    }

    private static void ResumeTime()
    {
        MyTime.isWorking = true;
    }

    private void Start()
    {
        MyTime.StartTime();
    }

    private void Update()
    {
        if (! GameState.IsInPlayState())
            return;
        
        MyTime.dt = Time.deltaTime;
        MyTime.t += MyTime.dt;
    }
}