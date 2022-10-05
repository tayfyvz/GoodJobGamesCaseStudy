namespace TadPoleFramework.Core
{
    public interface IReceivable
    {
        void Receive(BaseEventArgs baseEventArgs);
    }
}