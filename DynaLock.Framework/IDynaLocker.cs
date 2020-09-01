using System;

namespace DynaLock.Framework
{
    public interface IDynaLocker : IDisposable
    {
        object MetaData
        {
            get;
            set;
        }

        bool IsLockOwned();
    }
}
