using UnityEngine;
using UnityEngine.UI;

namespace TadPoleFramework.Utility
{
    [RequireComponent(typeof(Text))]
    public class AppVersionText : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Text>().text = $"V {Application.version}";
        }
    }
}