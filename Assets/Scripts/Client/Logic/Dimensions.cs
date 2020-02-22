using UnityEngine;
using Foundation;

public class Dimensions : MonoBehaviour
{
    private static Dimensions instance;

    public static float LEFT_EDGE    { get { return Dimensions.instance.leftEdge.position.x; } }
    public static float RIGHT_EDGE   { get { return Dimensions.instance.rightEdge.position.x; } }
    public static float LEFT_SPAWN   { get { return Dimensions.instance.leftSpawn.position.x; } }
    public static float RIGHT_SPAWN  { get { return Dimensions.instance.rightSpawn.position.x; } }
    public static float TOP_EDGE     { get { return Dimensions.instance.topEdge.position.y; } }
    public static float BOTTOM_EDGE  { get { return Dimensions.instance.bottomEdge.position.y; } }
    public static float MEDUSA_LIMIT  { get { return Dimensions.instance.medusaLimit.position.y; } }
    public static float WATER_SURFACE { get { return Dimensions.instance.waterSurface.position.y; } }

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    [SerializeField] private Transform topEdge;
    [SerializeField] private Transform bottomEdge;
    [SerializeField] private Transform medusaLimit;
    [SerializeField] private Transform waterSurface;

    private void Awake()
    {
        Dimensions.instance = this;
    }
}