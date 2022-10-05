using System;
using System.Collections;
using UnityEngine;

namespace TadPoleFramework.Utility
{
    public static class CoroutineUtility
    {
        public static void StopAndNullCoroutine(this Coroutine coroutine, MonoBehaviour gameObject)
        {
            if (coroutine != null)
            {
                gameObject.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        
        public static IEnumerator WaitAndDo(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        public static IEnumerator WaitForEndOfFrame(Action callback)
        {
            yield return new WaitForEndOfFrame();
            callback();
        }

        public static IEnumerator WaitForEndOfNFrame(int frameCount, Action callback)
        {
            for (int i = 0; i < frameCount; i++)
            {
                yield return null;
            }
            callback();
        }

        public static IEnumerator WaitUntil(Func<bool> action, Action callback)
        {
            yield return new WaitUntil(action);
            callback();
        }
        
        public static IEnumerator WaitUntil(Func<bool> action, Action<Action> callback, Action nextCallback)
        {
            yield return new WaitUntil(action);
            callback(nextCallback);
        }
        
        // public static IEnumerator WaitUntil<T>(Func<bool> action, Action<T> callback)
        // {
        //     yield return new WaitUntil(action);
        //     
        //     callback();
        // }

        public static IEnumerator WaitForRealTime(float delay)
        {
            while (true)
            {
                float pauseEndTime = Time.realtimeSinceStartup + delay;
                while (Time.realtimeSinceStartup < pauseEndTime)
                {
                    yield return 0;
                }
                break;
            }
        }
    }
}