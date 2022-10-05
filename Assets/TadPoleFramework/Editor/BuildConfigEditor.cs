using TadPoleFramework.Config;
using UnityEditor;
using UnityEngine;

namespace TadPoleFramework.Editor
{
    public class BuildConfigEditor : EditorWindow
    {
        private static BuildConfigEditor _window;

        private static AppConfig _appConfig;


        [MenuItem("TadPole/Build Config Editor")]
        private static void ShowWindow()
        {
            _window = GetWindow<BuildConfigEditor>("Build Config Editor", true);
        }

        public static bool IsInit
        {
            get
            {
                if (_appConfig == null)
                {
                    _appConfig = AssetDatabase.LoadAssetAtPath<AppConfig>("Assets/_GameFiles/Resources/AppConfig.asset");
                }

                return _appConfig != null;
                ;
            }
        }

        private void OnGUI()
        {
            if (_window == null)
                ShowWindow();

            EditorGUILayout.Space(20);

            if (!IsInit)
            {
                EditorGUILayout.HelpBox("AppConfig is not found in Assets/_GameFiles/Resources", MessageType.Warning);
                
                return;
            }

            EditorGUILayout.BeginVertical();
            
            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.TextField("ProductName", PlayerSettings.productName);
            
            EditorGUILayout.TextField("Android Package Name", PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android));
            EditorGUILayout.TextField("iOS Package Name", PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.iOS));
            
            EditorGUILayout.TextField("Version", PlayerSettings.bundleVersion);
            
            EditorGUILayout.TextField("Android Build Number", PlayerSettings.Android.bundleVersionCode.ToString());
            EditorGUILayout.TextField("iOS Build Number", PlayerSettings.iOS.buildNumber);
            
            
            
            EditorGUI.EndDisabledGroup();
            
            _appConfig.AppName = EditorGUILayout.TextField("Product Name", _appConfig.AppName);

            _appConfig.AndroidPackageName = EditorGUILayout.TextField("Android Package Name", _appConfig.AndroidPackageName);
            
            _appConfig.iOSPackageName = EditorGUILayout.TextField("iOS Package Name", _appConfig.iOSPackageName);
            
            _appConfig.Version = EditorGUILayout.IntField("Version", _appConfig.Version);
            
            _appConfig.Feature = EditorGUILayout.IntField("Feature", _appConfig.Feature);
            
            _appConfig.BuildCount = EditorGUILayout.IntField("Build Number", _appConfig.BuildCount);
            
            _appConfig.iOSBuildNumber = EditorGUILayout.IntField("iOS Build Number", _appConfig.iOSBuildNumber);
            _appConfig.AndroidBuildNumber = EditorGUILayout.IntField("Android Build Number", _appConfig.AndroidBuildNumber);
            
            EditorGUILayout.LabelField($"Version: {_appConfig.Version}.{_appConfig.Feature}.{_appConfig.BuildCount}");
            
            EditorGUILayout.Space(20);

            if (GUILayout.Button("Apply"))
            {
                PlayerSettings.productName = _appConfig.AppName;
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, _appConfig.AndroidPackageName);
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, _appConfig.iOSPackageName);
                PlayerSettings.bundleVersion = $"{_appConfig.Version}.{_appConfig.Feature}.{_appConfig.BuildCount}";
                PlayerSettings.Android.bundleVersionCode = _appConfig.AndroidBuildNumber;
                PlayerSettings.iOS.buildNumber = _appConfig.iOSBuildNumber.ToString();
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorUtility.SetDirty(this);
                EditorUtility.SetDirty(_appConfig);
                
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }


            EditorGUILayout.EndVertical();
        }
    }
}