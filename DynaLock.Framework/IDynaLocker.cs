using System;

namespace DynaLock.Framework
{
    public interface IDynaLocker : IDisposable
    {
        bool IsLockOwned();
    }
}
