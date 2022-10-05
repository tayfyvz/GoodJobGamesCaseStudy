using UnityEngine;

namespace TadPoleFramework.Config
{
    [CreateAssetMenu(fileName = "AppConfig", menuName = "TadPole/Create/AppConfig")]
    public class AppConfig : ScriptableObject
    {
        public string AppName;
        public string AndroidPackageName;
        public string iOSPackageName;
        public int Version;
        public int Feature;
        public int BuildCount;
        public int iOSBuildNumber;
        public int AndroidBuildNumber;
    }
}