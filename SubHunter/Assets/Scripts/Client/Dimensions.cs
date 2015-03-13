using UnityEngine;
using Foundation;

public class Dimensions
{
    private static float topEdge;
    public static float TOP_EDGE { get { return Dimensions.topEdge; } }

    private static float bottomEdge;
    public static float BOTTOM_EDGE { get { return Dimensions.bottomEdge; } }

    private static float leftEdge;
    public static float LEFT_EDGE { get { return Dimensions.leftEdge; } }

    private static float rightEdge;
    public static float RIGHT_EDGE { get { return Dimensions.rightEdge; } }

    private static float waterSurface;
    public static float WATER_SURFACE { get { return Dimensions.waterSurface; } }

    private static float screenLeft;
    public static float SCREEN_LEFT { get { return Dimensions.SCREEN_LEFT; } }

    private static float screenRight;
    public static float SCREEN_RIGHT { get { return Dimensions.SCREEN_RIGHT; } }

    private static float medusaSurface;
    public static float MEDUSA_SURFACE { get { return Dimensions.medusaSurface; } }

    public static void Init()
    {
        var width = Screen.width;
        var height = Screen.height;

        Log.Trace("Screen width = {0}, height = {1}.", width, height);

        var offsetX = width / 10;
        var offsetY = height / 10;

        var camera = Camera.main;
        var worldPt = camera.ScreenToWorldPoint(new Vector3(-offsetX, -offsetY, 0f));
        Dimensions.leftEdge = worldPt.x;
        Dimensions.bottomEdge = worldPt.y;

        worldPt = camera.ScreenToWorldPoint(new Vector3(width + offsetX, height + offsetY, 0f));
        Dimensions.rightEdge = worldPt.x;
        Dimensions.topEdge = worldPt.y;

        worldPt = camera.ScreenToWorldPoint(Vector3.zero);
        Dimensions.screenLeft = worldPt.x;

        worldPt = camera.ScreenToWorldPoint(new Vector3(width, 0f, 0f));
        Dimensions.screenRight = worldPt.x;

        worldPt = camera.ScreenToWorldPoint(new Vector3(0f, height - offsetY * 1.5f, 0f));
        Dimensions.waterSurface = worldPt.y;

        worldPt = camera.ScreenToWorldPoint(new Vector3(0f, height - offsetY * 2.5f, 0f));
        Dimensions.medusaSurface = worldPt.y;
    }
}