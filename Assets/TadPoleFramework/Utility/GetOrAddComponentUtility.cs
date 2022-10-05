using UnityEngine;

namespace TadPoleFramework.Utility
{
    public static class GetOrAddComponentUtility
    {
        /// <summary>
        /// Gets or add a component. Usage example:
        /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>(gameobject);
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.AddComponent<T>();
            }
            return result;
        }


        /// <summary>
        /// Remove a component. Usage example:
        /// transform.RemoveComponent<BoxCollider>(gameobject);
        /// </summary>
        public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
        {
            T result = gameObject.GetComponent<T>();
            if(result != null)
            {
                GameObject.Destroy(result);
            }
        }
    }
}