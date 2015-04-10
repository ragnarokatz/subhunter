using UnityEngine;
using Foundation;

public class Dimensions
{
    // X dimensions
    private static float leftEdge;
    public static float LEFT_EDGE { get { return Dimensions.leftEdge; } }

    private static float rightEdge;
    public static float RIGHT_EDGE { get { return Dimensions.rightEdge; } }

    private static float screenLeft;
    public static float SCREEN_LEFT { get { return Dimensions.SCREEN_LEFT; } }

    private static float screenRight;
    public static float SCREEN_RIGHT { get { return Dimensions.SCREEN_RIGHT; } }

    // Y Dimenions
    public const float WATER = 2.65f;
    public const float MEDUSA = 2.35f;
    public const float TOP_EDGE = 5f;
    public const float BOT_EDGE = -5f;
    public const float AMMO = 3.5f;

    static Dimensions()
    {
        Init();
    }

    private static void Init()
    {
        var width = Screen.width;
        var height = Screen.height;

        Log.Trace("Screen resolution: {0} x {1}.", width, height);

        var offset = (float) width / 5f;

        var camera = Camera.main;
        var worldPt = camera.ScreenToWorldPoint(Vector3.zero);
        Dimensions.screenLeft = worldPt.x;

        worldPt = camera.ScreenToWorldPoint(new Vector3(width, 0f, 0f));
        Dimensions.screenRight = worldPt.x;

        worldPt = camera.ScreenToWorldPoint(new Vector3(-offset, 0f, 0f));
        Dimensions.leftEdge = worldPt.x;

        worldPt = camera.ScreenToWorldPoint(new Vector3(width + offset, 0f, 0f));
        Dimensions.rightEdge = worldPt.x;
    }
}