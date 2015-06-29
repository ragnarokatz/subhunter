using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUD : MonoBehaviour
{
    public float  Duration;

    private bool  started;
    private float startTime;


    public void SetHUD(int points, int multiplier, Vector3 worldPoint)
    {
        this.gameObject.SetActive(true);

        var text = GetComponent<Text>();
        text.text = String.Format("{0} x {1}", points, multiplier);
        AlignScoreToEnemy(worldPoint);

        this.started = true;
        this.startTime = Time.time;
    }

    private void AlignScoreToEnemy(Vector3 worldPoint)
    {
        var canvasRect = HUDControls.I.Canvas.GetComponent<RectTransform>();
        var camera = HUDControls.I.GameCamera;
        var viewportPos = camera.WorldToViewportPoint(worldPoint);
        var screenPos = new Vector2(
            ((viewportPos.x * canvasRect.sizeDelta.x)-(canvasRect.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvasRect.sizeDelta.y)-(canvasRect.sizeDelta.y * 0.5f)));
        
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = screenPos;
    }

    private void Update()
    {
        if (! this.started)
            return;

        if (Time.time - this.startTime < this.Duration)
            return;

        GameObject.Destroy(this.gameObject);
    }
}