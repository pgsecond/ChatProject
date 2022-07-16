using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Assets.Helpers
{
    public static class ConnectionSettingsHelper
    {
        public static ConnectionSettings LoadConnectionSettings()
        {
            var path = Path.Combine(Application.streamingAssetsPath, "ConnectionSettings.json");

            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    return JsonConvert.DeserializeObject<ConnectionSettings>(sr.ReadToEnd());
                }
            }
        }
    }
}
