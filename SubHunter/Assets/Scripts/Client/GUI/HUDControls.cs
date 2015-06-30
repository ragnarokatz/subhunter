using System;
using UnityEngine;

public class HUDControls : MonoBehaviour
{
    private static HUDControls instance;
    public static HUDControls I { get { return HUDControls.instance; } }
    
    public void InstantiateScoreHUD(int points, int multiplier, Vector3 worldPoint)
    {
        var go = GameObject.Instantiate(this.Template) as GameObject;
        go.transform.SetParent(EntityManager.I.HUDParent);
        var hud = go.GetComponent<HUD>();
        hud.SetHUD(points, multiplier, worldPoint);
    }

    public Canvas     Canvas;
    public Camera     GameCamera;
    public GameObject Template;

    private void Start()
    {
        HUDControls.instance = this;
    }
}