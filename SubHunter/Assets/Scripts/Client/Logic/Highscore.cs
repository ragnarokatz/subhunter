using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Foundation;

public class Highscore
{
    static Highscore()
    {
        Highscore.configPath = Path.Combine(Application.persistentDataPath, "Highscore.json");
        Highscore.config  = IOCore.I.LoadConfig(configPath);
        if (Highscore.config == null)
        {
            Highscore.config = new Dictionary<string, object>();
            Highscore.config["score"] = 0;
        }
    }

    private static string configPath;
    private static Dictionary<string, object> config;

    public static int Score { get { return (int) Highscore.config["score"]; } }

    public static bool TrySubmitHighscore(int score)
    {
        if (score <= Highscore.Score)
            return false;

        Highscore.config["score"] = score;
        IOCore.I.SaveConfig(Highscore.configPath, Highscore.config);
        return true;
    }

    // Old design ////----
    #if false
    private class Record
    {
        public string name;
        public int    score;

        public Record(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    private class Sorter : IComparer<Record>
    {
        int IComparer<Record>.Compare(Record a, Record b)
        {
            if (a.score > b.score)
                return -1;
            else if (a.score < b.score)
                return 1;
            else
                return 0;
        }
    }

    private Sorter sorter = new Sorter();

    private Dictionary<string, object> highscores;
    private List<Record>               records;

    public void SubmitHighscore(string name, int score)
    {
        var record = new Record(name, score);
        this.records.Add(record);
        this.records.Sort();

        this.highscores.Clear();
        var count = this.records.Count > 10 ? 10 : this.records.Count;
        for (int i = 0; i < count; i++)
        {
            record = this.records[i];
            this.highscores.Add(record.name, record.score);
        }

        IOCore.I.SaveConfig(this.configPath, this.highscores);
    }

    private HighscoreConfig()
    {
        this.highscores = IOCore.I.LoadConfig(configPath) ?? new Dictionary<string, object>(10);
        this.records = new List<Record>(10);

        foreach (var kvp in this.highscores)
        {
            var name   = kvp.Key;
            var score  = (int) kvp.Value;
            var record = new Record(name, score);
            this.records.Add(record);
        }

        this.records.Sort(this.sorter);
    }
    #endif
}

