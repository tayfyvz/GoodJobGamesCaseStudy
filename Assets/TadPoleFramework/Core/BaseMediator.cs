using System.Collections.Generic;
using System.Linq;

namespace TadPoleFramework.Core
{
    public class BaseMediator : IMediator
    {
        private List<IReceivable> _receivables = new List<IReceivable>();
        
        public void Add(IReceivable receivable)
        {
            if (!_receivables.Contains(receivable))
                _receivables.Add(receivable);
        }

        public virtual void Broadcast(IReceivable receivable, BaseEventArgs baseEventArgs)
        {
            foreach (var otherReceivable in _receivables.Where(otherReceivable => otherReceivable != receivable))
                otherReceivable.Receive(baseEventArgs);
        }
    }
}