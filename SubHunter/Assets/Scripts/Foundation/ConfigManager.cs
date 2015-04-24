using System;
using System.IO;
using System.Collections.Generic;
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

        private Dictionary<string, object> allConfigs;
        private bool isInitialized = false;

        private ConfigManager()
        {
            Log.Assert(! this.isInitialized);

            var configPath = Path.Combine(UnityEngine.Application.dataPath, "Configs");
            LoadConfigs(configPath);

            this.isInitialized = true;
        }

        private void LoadConfigs(string configPath)
        {
            this.allConfigs = new Dictionary<string, object>();

            var paths = Directory.GetFiles(configPath, "*.txt");
            var files = GetFiles(paths);

            System.Diagnostics.Debug.Assert(files.Length == paths.Length);

            for (int i = 0; i < paths.Length; i++)
            {
                var path = paths[i];
                var json = File.ReadAllText(path);
                var config = Json.JsonReader.Deserialize<Dictionary<string, object>>(json);
                this.allConfigs.Add(files[i], config);
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

        public Dictionary<string, object> GetConfig(string fileName)
        {
            if (! this.allConfigs.ContainsKey(fileName))
            {
                Log.Error("Config with name {0} is not found.", fileName);
                return null;
            }
            return this.allConfigs[fileName] as Dictionary<string, object>;
        }
    }
}