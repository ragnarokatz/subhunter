using System;
using System.IO;
using Json = Pathfinding.Serialization.JsonFx;

namespace Foundation
{
    /// <summary>
    /// Json config manager.
    /// </summary>
    public class ConfigManager
    {
        private static ConfigManager instance = new ConfigManager();
        public static ConfigManager I { get { return ConfigManager.instance; } }

        private Dbase allConfigs;
        private bool isInitialized = false;

        public void Init(string configPath)
        {
            Log.Assert(! this.isInitialized);

            this.isInitialized = true;
            LoadConfigs(configPath);
        }

        private ConfigManager()
        {
        }

        private void LoadConfigs(string configPath)
        {
            this.allConfigs = new Dbase();

            var paths = Directory.GetFiles(configPath, "*.json");
            var files = GetFiles(paths);

            System.Diagnostics.Debug.Assert(files.Length == paths.Length);

            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                var json = File.ReadAllText(path);
                var config = Json.JsonReader.Deserialize<Dbase>(json);
                this.allConfigs.Set(files[i], config);
            }
        }

        private string[] GetFiles(string[] paths)
        {
            string[] fileNames = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                fileNames[i] = Path.GetFileNameWithoutExtension(path);
            }
            return fileNames;
        }

        public Dbase GetConfig(string fileName)
        {
            if (! this.allConfigs.Contains(fileName))
            {
                Log.Error("Config with name {0} is not found.", fileName);
                return null;
            }
            return this.allConfigs.QueryDbase(fileName);
        }
    }
}