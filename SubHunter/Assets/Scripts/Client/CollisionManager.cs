using UnityEngine;
using System;

public class CollisionManager
{
    private static UnityEngine.Object explodePrefab = Resources.Load("Explosion");
    public static void StartExplosion(Vector3 pos)
    {
        GameObject.Instantiate(explodePrefab, pos, Quaternion.identity);
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}