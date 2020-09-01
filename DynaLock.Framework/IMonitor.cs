namespace DynaLock.Framework
{
    public interface IMonitor
    {
        void Enter();
        bool TryEnter(int millisecondsTimeout = 0);
        void Exit();
    }
}
