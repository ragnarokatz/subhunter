using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Foundation;

public class HighscoreConfig
{
    private static HighscoreConfig instance = new HighscoreConfig();
    public static HighscoreConfig I { get { return HighscoreConfig.instance; } }

    private string configPath;
    private Dictionary<string, object> highscore;

    public int Highscore { get { return (int) this.highscore["score"]; } }

    public bool TrySubmitHighscore(int score)
    {
        if (score <= this.Highscore)
            return false;

        this.highscore["score"] = score;
        IOCore.I.SaveConfig(this.configPath, this.highscore);
        return true;
    }

    private HighscoreConfig()
    {
        this.configPath = Path.Combine(Application.persistentDataPath, "Highscore.json");
        this.highscore  = IOCore.I.LoadConfig(configPath) ?? new Dictionary<string, object>();
    }

    #if false // Old design ////----
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

