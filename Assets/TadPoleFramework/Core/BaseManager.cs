using System.Collections.Generic;
using UnityEngine;

namespace TadPoleFramework.Core
{
    public abstract class BaseManager : MonoBehaviour, IReceivable, IManageable
    {
        protected BaseManager _manager;
        protected IMediator _mediator;

        protected List<IReceivable> receivables = new List<IReceivable>();

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
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

        public void Add(IReceivable receivable)
        {
            if (!receivables.Contains(receivable))
                receivables.Add(receivable);
            else
                Debug.LogError(
                    $"[{nameof(this.receivables)}] already added to [{this.GetType().Name}]");
        }

        public void InjectMediator(IMediator mediator)
        {
            if (_mediator != null)
                Debug.LogError(
                    $"[{nameof(this._mediator)}] already added to [{this.GetType().Name}]");
            else
            {
                _mediator = mediator;
                _mediator.Add(this);
            }
        }

        public abstract void Receive(BaseEventArgs baseEventArgs);

        protected virtual void BroadcastUpward(BaseEventArgs baseEventArgs)
        {
            _manager.Receive(baseEventArgs);
        }

        protected virtual void Broadcast(BaseEventArgs baseEventArgs)
        {
            if (_mediator == null)
                Debug.LogWarning($"No {nameof(_mediator)} found for {this.GetType().Name}");
            else
                _mediator.Broadcast(this, baseEventArgs);
        }

        protected virtual void BroadcastDownward(BaseEventArgs baseEventArgs)
        {
            foreach (var receivable in receivables)
                receivable.Receive(baseEventArgs);
        }
    }
}