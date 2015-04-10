using UnityEngine;
using System.IO;
using Foundation;

public class Init : MonoBehaviour
{
    private void HandleOnLog(Log.LogTypes type, string message)
    {
        switch (type)
        {
        case Log.LogTypes.Trace:
        default:
            Debug.Log(message);
            break;
        case Log.LogTypes.Error:
            Debug.LogError(message);
            break;
        case Log.LogTypes.Warning:
            Debug.LogWarning(message);
            break;
        }
    }

    void Start()
    {
        Log.OnLog += HandleOnLog;

        Game.StartGame();
    }

    void OnDestroy()
    {
        Log.OnLog -= HandleOnLog;
    }
}
