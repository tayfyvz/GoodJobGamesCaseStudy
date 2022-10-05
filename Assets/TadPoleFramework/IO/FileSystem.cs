using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace TadPoleFramework.IO
{
    public static class FileSystem
    {
        public static void Save<T>(string filePath, T value)
        {
            string content = JsonConvert.SerializeObject(value);
            string name = Path.GetFileName(filePath);
            string path = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            System.IO.File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        public static T Load<T>(string filePath)
        {
            string json = ReadAllText(filePath);

            if (json == "")
                return default;

            // Debug.Log($"Json: {json}");
            
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ReadAllText(string filePath)
        {
#if UNITY_EDITOR || UNITY_IOS
            return File.ReadAllText(filePath);
#elif UNITY_ANDROID
            return ReadAllTextOnAndroid(filePath);
#endif
        }

        private static string ReadAllTextOnAndroid(string filePath)
        {
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                WWW www = new WWW(filePath);

                while (!www.isDone)
                {

                }
                return System.Text.Encoding.UTF8.GetString(www.bytes, 0, www.bytes.Length);

            }
            else
            {
                return File.ReadAllText(filePath);
            }
        }
    }
}