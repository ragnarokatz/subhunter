using UnityEngine;

public class Explosion
{
    private static Object prefab = Resources.Load("Explosion");

    public static void StartExplosion(Vector3 pos)
    {
        GameObject.Instantiate(prefab, pos, Quaternion.identity);
    }
}

