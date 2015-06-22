using System;
using System.IO;
using System.Collections.Generic;
using Json = Pathfinding.Serialization.JsonFx;

namespace Foundation
{
    public class IOCore
    {
        private static IOCore instance = new IOCore();
        public static IOCore I { get { return IOCore.instance; } }

        public void SaveConfig(string path, Dictionary<string, object> config)
        {
            using (var jr = new Json.JsonWriter(path))
            {
                jr.Settings.PrettyPrint = true;
                jr.Write(config);
            }
        }

        public Dictionary<string, object> LoadConfig(string path)
        {
            if (! File.Exists(path))
            {
                Log.Trace("File does not exist at path {0}.", path);
                return null;
            }

            var json = File.ReadAllText(path);
            if (json == null)
            {
                Log.Trace("File is empty at path {0}.", path);
                return null;
            }

            return Json.JsonReader.Deserialize<Dictionary<string, object>>(json);
        }
    }
}