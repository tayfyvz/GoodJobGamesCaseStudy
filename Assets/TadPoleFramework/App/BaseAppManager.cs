using DG.Tweening;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework.App
{
    public abstract class BaseAppManager : BaseManager
    {
        protected virtual void Awake()
        {
            DOTween.SetTweensCapacity(1000, 1000);
            Application.targetFrameRate = 60;
        }
    }
}