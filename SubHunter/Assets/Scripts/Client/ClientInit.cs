using UnityEngine;
using Foundation;

public class ClientInit : MonoBehaviour
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

    private void Start()
    {
        Log.OnLog += HandleOnLog;

        Dimensions.Init();

        var configAssets = Resources.LoadAll("Configs", typeof(TextAsset));
        foreach (TextAsset asset in configAssets)
            ConfigManager.I.LoadConfig(asset.name, asset.text);
    }

    private void OnDestroy()
    {
        Log.OnLog -= HandleOnLog;
    }
}
