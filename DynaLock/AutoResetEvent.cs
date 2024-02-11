using DynaLock.Framework;
using SystemAutoResetEvent = System.Threading.AutoResetEvent;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker AutoResetEvent to create and manage locker objects dynamically in run-time
    /// </summary>
    public class AutoResetEvent : DynaLocker<SystemAutoResetEvent>, IAutoResetEvent
    {
        private static IContext<SystemAutoResetEvent> _defaultContext = new Context.Context<SystemAutoResetEvent>();
        private readonly SystemAutoResetEvent _currentObject;

        /// <summary>
        /// Constructor of AutoResetEvent class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new AutoResetEvent</param>
        public AutoResetEvent(Context.Context<SystemAutoResetEvent> context, string name) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var currentManualAutoReset))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out currentManualAutoReset))
                    {
                        _currentObject = new SystemAutoResetEvent(true);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (currentManualAutoReset != null)
                _currentObject = currentManualAutoReset;
        }

        public bool WaitOne()
        {
            if (_currentObject.WaitOne())
                IsLockOwnedFlag = true;

            return IsLockOwnedFlag;
        }

        public bool WaitOne(int millisecondsTimeout)
        {
            if (_currentObject.WaitOne(millisecondsTimeout))
                IsLockOwnedFlag = true;

            return IsLockOwnedFlag;
        }

        public void Set()
        {
            if (IsLockOwnedFlag)
            {
                _currentObject.Set();
                IsLockOwnedFlag = false;
            }
        }

        public override void Dispose()
        {
            Set();
        }
    }
}
