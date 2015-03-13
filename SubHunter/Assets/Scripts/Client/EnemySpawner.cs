using UnityEngine;
using Foundation;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Scout;
    public GameObject Torpedo;
    public GameObject Bonus;
    public GameObject Missile;
    public GameObject Medusa;
    public GameObject Firefish;

    private float startTime;
    private float endTime;

    private float elapsedTime { get { return Time.time - this.startTime; } }
    private Dictionary<string, object> sets;

    private List<string> spawned;

    void Start()
    {
        var levelInfo = ConfigManager.I.GetConfig("Level1"); ////----
        var length = (float) (int) levelInfo["Length"];

        var sets = levelInfo["Sets"] as Dictionary<string, object>;
        this.sets = sets;

        this.startTime = Time.time;
        this.endTime = this.startTime + length;

        this.spawned = new List<string>();
    }

    void Update()
    {
        foreach (var kvp in this.sets)
        {
            float result;
            if (! float.TryParse(kvp.Key, out result))
                continue;

            if (result > this.elapsedTime)
                continue;

            var info = kvp.Value as Dictionary<string, object>;
            foreach (var kvp2 in info)
                Spawn(kvp2.Key, (int) kvp2.Value);

            this.spawned.Add(kvp.Key);
        }

        if (this.spawned.Count == 0)
            return;

        foreach (var i in spawned)
            this.sets.Remove(i);

        this.spawned.Clear();
    }

    private void Spawn(string type, int count)
    {
        for (int i = 0; i < count; i++)
            Spawn(type);
    }

    private void Spawn(string type)
    {
        switch (type)
        {
        case "Scout":
            GameObject.Instantiate(Scout);
            break;

        case "Bonus":
            GameObject.Instantiate(Bonus);
            break;

        case "Torpedo":
            GameObject.Instantiate(Torpedo);
            break;

        case "Missile":
            GameObject.Instantiate(Missile);
            break;

        case "Jellyfish":
            GameObject.Instantiate(Medusa);
            break;

        case "Firefish":
            GameObject.Instantiate(Firefish);
            break;
        }
    }

}
