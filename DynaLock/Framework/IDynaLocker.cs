using System;

namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLocker interface
    /// </summary>
    public interface IDynaLocker<TType> : IDisposable
    {
        /// <summary>
        /// An object to store meta data in current context.
        /// </summary>
        object MetaData
        {
            get;
            set;
        }

        /// <summary>
        /// Return true if the locker is taken by current thread.
        /// </summary>
        /// <returns></returns>
        bool IsLockOwned();

        /// <summary>
        /// Returns current context
        /// </summary>
        IContext<TType> GetContext
        {
            get;
        }
    }
}
