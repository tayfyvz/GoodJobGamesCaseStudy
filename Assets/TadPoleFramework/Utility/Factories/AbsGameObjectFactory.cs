using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TadPoleFramework.Utility.Factories
{
    public abstract class AbsGameObjectFactory<T, TArg> : MonoBehaviour where T : Component, IFactoryItem<TArg>
        where TArg : IFactoryItemArg
    {
        [SerializeField] private T _prefab;
        
        public T GetItem<T>(TArg arg, Transform parentContainer) where T : IFactoryItem<TArg>
        {
            var tmp = Instantiate(_prefab.gameObject, Vector3.zero, Quaternion.identity, parentContainer);
            tmp.transform.localScale = Vector3.one;
            tmp.SetActive(false);
            var result = tmp.GetComponent<T>();
            result.ReInitialize(arg);

            return result;
        }

        public T GetItem<T>(TArg arg) where T : IFactoryItem<TArg>
        {
            var tmp = Instantiate(_prefab.gameObject, Vector3.zero, Quaternion.identity);
            tmp.transform.localScale = Vector3.one;
            tmp.SetActive(false);
            var result = tmp.GetComponent<T>();
            result.ReInitialize(arg);

            return result;
        }
    }

    public abstract class AbsGameObjectFactory<T, TArg, TEnum> : MonoBehaviour
        where T : Component, IFactoryItem<TArg, TEnum>
        where TArg : IFactoryItemArg
        where TEnum : struct, Enum
    {
        [SerializeField] private List<T> _prefabs;

        private TPrefab GetPrefab<TPrefab>(TEnum prefabType) where TPrefab : Component, IFactoryItem<TArg, TEnum>
        {
            return _prefabs.FirstOrDefault(x => x.PrefabType.Equals(prefabType)) as TPrefab;
        }

        public TPrefab GetItem<TPrefab>(TEnum prefabType, TArg arg, Transform parentContainer)
            where TPrefab : Component, IFactoryItem<TArg, TEnum>
        {
            TPrefab tmpPrefab = GetPrefab<TPrefab>(prefabType);
            if (tmpPrefab == null)
                throw new Exception($"Prefab of type {prefabType} not found");
            var tmp = Instantiate(tmpPrefab.gameObject, Vector3.zero, Quaternion.identity, parentContainer);
            tmp.transform.localScale = Vector3.one;
            tmp.SetActive(false);
            var result = tmp.GetComponent<TPrefab>();
            result.ReInitialize(arg);

            return result;
        }
        
        public TPrefab GetItem<TPrefab>(TEnum prefabType, TArg arg)
            where TPrefab : Component, IFactoryItem<TArg, TEnum>
        {
            TPrefab tmpPrefab = GetPrefab<TPrefab>(prefabType);
            if (tmpPrefab == null)
                throw new Exception($"Prefab of type {prefabType} not found");
            var tmp = Instantiate(tmpPrefab.gameObject, Vector3.zero, Quaternion.identity);
            tmp.transform.localScale = Vector3.one;
            tmp.SetActive(false);
            var result = tmp.GetComponent<TPrefab>();
            result.ReInitialize(arg);

            return result;
        }
    }
}