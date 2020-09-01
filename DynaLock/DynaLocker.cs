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

        public object MetaData
        {
            get => ContextMapper.Invoke(_context).MetaData;
            set => ContextMapper.Invoke(_context).MetaData = value;
        }

        public bool IsLockOwned() => _isLockOwned;

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
