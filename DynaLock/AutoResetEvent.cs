using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker AutoResetEvent to create and manage locker objects dynamically in run-time
    /// </summary>
    public class AutoResetEvent : DynaLocker, IAutoResetEvent
    {
        private static IContext _defaultContext = new Context.AutoResetEvent();
        private readonly System.Threading.AutoResetEvent _currentObject;

        /// <summary>
        /// Constructor of AutoResetEvent class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new AutoResetEvent</param>
        public AutoResetEvent(Context.AutoResetEvent context, string name) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var tempSemaphore))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out tempSemaphore))
                    {
                        _currentObject = new System.Threading.AutoResetEvent(true);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (tempSemaphore != null)
                _currentObject = (System.Threading.AutoResetEvent)tempSemaphore;
        }

        public bool WaitOne()
        {
            if (_currentObject.WaitOne())
                _isLockOwned = true;

            return _isLockOwned;
        }

        public bool WaitOne(int millisecondsTimeout)
        {
            if (_currentObject.WaitOne(millisecondsTimeout))
                _isLockOwned = true;

            return _isLockOwned;
        }

        public void Set()
        {
            if (_isLockOwned)
            {
                _currentObject.Set();
                _isLockOwned = false;
            }
        }

        public override void Dispose()
        {
            Set();
        }
    }
}
