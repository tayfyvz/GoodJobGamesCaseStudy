using TMPro;
using UnityEngine;

namespace TadPoleFramework.Utility
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AppVersionTextMeshPro : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"V {Application.version}";
        }
    }
}