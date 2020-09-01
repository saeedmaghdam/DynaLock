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
        protected bool _isLockOwned = false;
        private IContext _context;

        /// <summary>
        /// Constructor of DynaLocker class
        /// </summary>
        /// <param name="context"></param>
        public DynaLocker(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// An object to store meta data in current context.
        /// </summary>
        public object MetaData
        {
            get => ContextMapper.Invoke(_context).MetaData;
            set => ContextMapper.Invoke(_context).MetaData = value;
        }

        /// <summary>
        /// Return true if the locker is taken by current thread.
        /// </summary>
        /// <returns></returns>
        public bool IsLockOwned() => _isLockOwned;

        /// <summary>
        /// Dispose the current DynaLocker object
        /// </summary>
        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
