using UnityEngine;
using Foundation;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Scout;
    public GameObject Torpedo;
    public GameObject Bonus;
    public GameObject Missile;
    public GameObject Jellyfish;
    public GameObject Firefish;

    private float startTime;
    private float endTime;

    private float elapsedTime { get { return Time.time - this.startTime; } }
    private Dictionary<string, object> sets;

    private List<string> instantiated;

    void Start()
    {
        var levelInfo = ConfigManager.I.GetConfig("Level1");
        var length = (float) (int) levelInfo["Length"];

        var sets = levelInfo["Sets"] as Dictionary<string, object>;
        this.sets = sets;

        this.startTime = Time.time;
        this.endTime = this.startTime + length;

        this.instantiated = new List<string>();
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
            foreach (KeyValuePair<string, object> kvp2 in info)
                Instantiate(kvp2.Key, (int) kvp2.Value);

            this.instantiated.Add(kvp.Key);
        }

        if (this.instantiated.Count == 0)
            return;

        foreach (var i in instantiated)
            this.sets.Remove(i);

        this.instantiated.Clear();
    }

    private void Instantiate(string type, int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(type);
    }

    private void Instantiate(string type)
    {
        switch (type)
        {
        case "Scout":
            Instantiate(Scout);
            break;

        case "Bonus":
            Instantiate(Bonus);
            break;

        case "Torpedo":
            Instantiate(Torpedo);
            break;

        case "Missile":
            Instantiate(Missile);
            break;

        case "Jellyfish":
            Instantiate(Jellyfish);
            break;

        case "Firefish":
            Instantiate(Firefish);
            break;
        }
    }

}
