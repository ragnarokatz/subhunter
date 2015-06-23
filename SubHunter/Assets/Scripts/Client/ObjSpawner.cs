using UnityEngine;
using Foundation;
using System;
using System.Collections.Generic;

public class ObjSpawner : MonoBehaviour
{
    public Game       Game;
    public GameObject Ship;
    public GameObject Explosion;
    public GameObject Scout;
    public GameObject Torpedo;
    public GameObject Bonus;
    public GameObject Missile;
    public GameObject Medusa;
    public GameObject Firefish;

    private Dictionary<string, GameObject> spawnTypes;

    private bool  isWorking;
    private float startTime;
    private int[] times;
    private int   index;
    private Dictionary<string, object> spawnSets;

    public void StartSpawn()
    {
        this.isWorking = true;

        var level = Player.I.Level;
        var levelConfig = ConfigManager.I.GetConfig(String.Format("Level{0}", level));
        this.spawnSets = levelConfig["Sets"] as Dictionary<string, object>;

        this.times = new int[this.spawnSets.Keys.Count];
        var i = 0;
        foreach (var time in this.spawnSets.Keys)
        {
            this.times[i] = Int32.Parse(time);
            i++;
        }

        this.startTime = Time.time;
        this.index = 0;
    }

    public void FinishSpawn()
    {
        this.isWorking = false;
    }

    private void SpawnObjs()
    {
        var key = this.times[this.index].ToString();
        var dict = this.spawnSets[key] as Dictionary<string, object>;
        foreach (var kvp in dict)
        {
            var type = kvp.Key;
            var count = (int) kvp.Value;
            for (int i = 0; i < count; i++)
                GameObject.Instantiate(this.spawnTypes[type]);
        }
    }

    private void Start()
    {
        this.spawnTypes = new Dictionary<string, GameObject>(6);

        this.spawnTypes.Add("Scout", this.Scout);
        this.spawnTypes.Add("Missile", this.Missile);
        this.spawnTypes.Add("Torpedo", this.Torpedo);
        this.spawnTypes.Add("Bonus", this.Bonus);
        this.spawnTypes.Add("Firefish", this.Firefish);
        this.spawnTypes.Add("Medusa", this.Medusa);
    }

    private void Update()
    {
        if (! this.isWorking)
            return;

        if (this.index >= this.times.Length)
        {
            FinishSpawn();
            return;
        }

        var nextTime = this.times[this.index];
        if (Time.time - this.startTime < nextTime)
            return;

        SpawnObjs();
        this.index++;
    }
}
