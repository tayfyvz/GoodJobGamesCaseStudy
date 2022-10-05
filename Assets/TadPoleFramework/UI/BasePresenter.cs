using System.Collections.Generic;
using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework.UI
{
    public abstract class BasePresenter : MonoBehaviour, IReceivable, IManageable
    {
        [SerializeField] protected BaseView view;
        
        private BaseManager _manager;
        protected List<IReceivable> receivables = new List<IReceivable>();
        
        protected virtual void Awake()
        {
            view.InjectPresenter(this);
        }

        public void InjectManager(BaseManager manager)
        {
            if (_manager != null)
                Debug.LogError(
                    $"[{nameof(this._manager)}] already added to [{this.GetType().Name}]");
            else
            {
                _manager = manager;
                _manager.Add(this);
            }
        }
        
        protected virtual void BroadcastUpward(BaseEventArgs baseEventArgs)
        {
            if(_manager != null)
                _manager.Receive(baseEventArgs);
            else
                Debug.LogError($"UIManager not initialized (UIManager.Awake) to [{this.GetType().Name}]");            
        }

        public virtual void ShowView()
        {
            view.gameObject.SetActive(true);
        }

        public virtual void HideView()
        {
            view.gameObject.SetActive(false);
        }

        public abstract void Receive(BaseEventArgs baseEventArgs);
    }
}