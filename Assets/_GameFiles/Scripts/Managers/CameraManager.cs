using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework
{
    public class CameraManager : BaseManager
    {
        [SerializeField] private Camera camera;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case CameraSetterEventArgs cameraSetterEventArgs:
                    camera.transform.position = new Vector3(((float)(cameraSetterEventArgs.Column - 1) / 2), 20, 0);
                    int max = Mathf.Max(cameraSetterEventArgs.Column, cameraSetterEventArgs.Row);
                    camera.orthographicSize = max;
                    break;
            }
        }
    }
}