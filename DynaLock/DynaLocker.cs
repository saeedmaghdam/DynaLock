using DynaLock.Framework;
using System;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker base class
    /// </summary>
    public abstract class DynaLocker : IDynaLocker
    {
        protected Func<IContext, IContext> ContextMapper;
        protected bool IsLockOwnedFlag = false;
        protected IContext Context;

        /// <summary>
        /// Constructor of DynaLocker class
        /// </summary>
        /// <param name="context"></param>
        protected DynaLocker(IContext context)
        {
            Context = context;
        }

        /// <summary>
        /// An object to store meta data in current context.
        /// </summary>
        public object MetaData
        {
            get => ContextMapper.Invoke(Context).MetaData;
            set => ContextMapper.Invoke(Context).MetaData = value;
        }

        /// <summary>
        /// Return true if the locker is taken by current thread.
        /// </summary>
        /// <returns></returns>
        public bool IsLockOwned() => IsLockOwnedFlag;

        public IContext GetContext => ContextMapper.Invoke(Context);

        /// <summary>
        /// Dispose the current DynaLocker object
        /// </summary>
        public virtual void Dispose()
        {
            Context = null;
            ContextMapper = null;
            MetaData = null;
        }
    }
}
