using System;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    private static Prefabs instance;

    public static GameObject Ship      { get { return Prefabs.instance.ship; } }
    public static GameObject Explosion { get { return Prefabs.instance.explosion; } }

    public static GameObject Scout     { get { return Prefabs.instance.scout; } }
    public static GameObject Torpedo   { get { return Prefabs.instance.torpedo; } }
    public static GameObject Bonus     { get { return Prefabs.instance.bonus; } }
    public static GameObject Missile   { get { return Prefabs.instance.missile; } }
    public static GameObject Medusa    { get { return Prefabs.instance.medusa; } }
    public static GameObject Firefish  { get { return Prefabs.instance.firefish; } }

    public GameObject ship;
    public GameObject explosion;

    public GameObject scout;
    public GameObject torpedo;
    public GameObject bonus;
    public GameObject missile;
    public GameObject medusa;
    public GameObject firefish;

    private void Start()
    {
        System.Diagnostics.Debug.Assert(Prefabs.instance == null);

        Prefabs.instance = this;
    }
}