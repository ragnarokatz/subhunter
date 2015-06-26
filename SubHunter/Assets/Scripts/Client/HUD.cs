using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HUD : MonoBehaviour
{
    public void SetHUD(int points, int multiplier, Vector3 worldPoint)
    {
        this.gameObject.SetActive(true);

        var text = GetComponent<Text>();
        text.text = String.Format("{0} X {1}", points, multiplier);
        AlignScoreToEnemy(worldPoint);
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
}