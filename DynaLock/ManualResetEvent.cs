using DynaLock.Framework;
using SystemManualResetEvent = System.Threading.ManualResetEvent;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker ManualResetEvent to create and manage locker objects dynamically in run-time
    /// </summary>
    public class ManualResetEvent : DynaLocker<SystemManualResetEvent>, IManualResetEvent
    {
        private static IContext<SystemManualResetEvent> _defaultContext = new Context.Context<SystemManualResetEvent>();
        private readonly SystemManualResetEvent _currentObject;

        /// <summary>
        /// Constructor of ManualResetEvent class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new ManualResetEvent</param>
        /// <param name="initialState">Specifies whether wait handle initializes in signal mode or not</param>
        public ManualResetEvent(Context.Context<SystemManualResetEvent> context, string name, bool initialState) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var tempSemaphore))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out tempSemaphore))
                    {
                        _currentObject = new SystemManualResetEvent(initialState);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (tempSemaphore != null)
                _currentObject = tempSemaphore;
        }

        public bool WaitOne()
        {
            return _currentObject.WaitOne();
        }

        public bool WaitOne(int millisecondsTimeout)
        {
            return _currentObject.WaitOne(millisecondsTimeout);
        }

        public void Set()
        {
            _currentObject.Set();
        }

        public void Reset()
        {
            _currentObject.Reset();
        }

        public override void Dispose()
        {
        }
    }
}
