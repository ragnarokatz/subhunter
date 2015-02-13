﻿using UnityEngine;
using System.IO;
using Foundation;

public class Init : MonoBehaviour
{
    private static Init instance;
    public static Init I { get { return Init.instance; } }

    public GameObject PlayerShip;

    private const string CONFIG_PATH = "Configs";

    void Awake()
    {
        Init.instance = GameObject.Find("Init").GetComponent<Init>();

        Log.OnLog += HandleOnLog;

        var configPath = Path.Combine(UnityEngine.Application.dataPath, CONFIG_PATH);
        ConfigManager.I.Init(configPath);
    }

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

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        Log.OnLog -= HandleOnLog;
    }
}