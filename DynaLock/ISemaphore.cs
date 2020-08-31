using System;

namespace DynaLock
{
    public interface ISemaphore : IDisposable
    {
        bool IsLockOwned();
        void WaitOne(int millisecondsTimeout = 0);
        void Release();
    }
}
