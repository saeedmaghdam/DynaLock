using DynaLock.Framework;
using System;

namespace DynaLock
{
    public abstract class DynaLocker : IDynaLocker
    {
        protected Func<IContext, IContext> ContextMapper;
        protected bool _isLockOwned = false;
        private IContext _context;

        public DynaLocker(IContext context)
        {
            _context = context;
        }

        public bool IsLockOwned() => _isLockOwned;

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
