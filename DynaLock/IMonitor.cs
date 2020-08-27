using System;

namespace DynaLock
{
    public interface IMonitor : IDisposable
    {
        bool IsLockOwned();
        void Enter();
        bool TryEnter(int millisecondsTimeout = 0);
    }
}
