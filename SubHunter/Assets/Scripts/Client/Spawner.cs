using UnityEngine;
using Foundation;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Level))]
[RequireComponent(typeof(Game))]
public class Spawner : MonoBehaviour
{
    public GameObject Ship;
    public GameObject Explosion;
    public GameObject Scout;
    public GameObject Torpedo;
    public GameObject Bonus;
    public GameObject Missile;
    public GameObject Medusa;
    public GameObject Firefish;

    private Dictionary<string, GameObject> entityTypes;

    private bool  isWorking;
    private float startTime;
    private int[] times;
    private int   index;
    private Dictionary<string, object> entitySets;

    public void StartSpawn()
    {
        this.isWorking = true;

        var level = Player.I.Level;
        var levelConfig = ConfigManager.I.GetConfig(String.Format("Level{0}", level));
        this.entitySets = levelConfig["Sets"] as Dictionary<string, object>;

        this.times = new int[this.entitySets.Keys.Count];
        var i = 0;
        foreach (var time in this.entitySets.Keys)
        {
            this.times[i] = Int32.Parse(time);
            i++;
        }

        this.startTime = MyTime.time;
        this.index = 0;
    }

    public void FinishSpawn()
    {
        this.isWorking = false;
    }

    private void Start()
    {
        this.entityTypes = new Dictionary<string, GameObject>(6);

        this.entityTypes.Add("Scout", this.Scout);
        this.entityTypes.Add("Missile", this.Missile);
        this.entityTypes.Add("Torpedo", this.Torpedo);
        this.entityTypes.Add("Bonus", this.Bonus);
        this.entityTypes.Add("Firefish", this.Firefish);
        this.entityTypes.Add("Medusa", this.Medusa);
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
        if (MyTime.time - this.startTime < nextTime)
            return;

        var key = this.times[this.index].ToString();
        var dict = this.entitySets[key] as Dictionary<string, object>;
        foreach (var kvp in dict)
        {
            var type = kvp.Key;
            var count = (int) kvp.Value;
            for (int i = 0; i < count; i++)
                GameObject.Instantiate(this.entityTypes[type]);
        }

        this.index++;
    }
}
