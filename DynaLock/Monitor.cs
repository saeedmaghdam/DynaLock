using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker Monitor to create and manage locker objects dynamically in run-time
    /// </summary>
    public class Monitor : DynaLocker, IMonitor
    {
        private static IContext _defaultContext = new Context.Monitor();
        private readonly object _currentObject;

        /// <summary>
        /// Constructor of Monitor class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new Monitor</param>
        public Monitor(Context.Monitor context, string name) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out _currentObject))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out _currentObject))
                    {
                        _currentObject = new object();
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }
        }

        /// <summary>
        /// Wait until take the locker.
        /// </summary>
        public void Enter()
        {
            System.Threading.Monitor.Enter(_currentObject, ref IsLockOwnedFlag);
        }

        /// <summary>
        /// Tries to take the locker in given timeout in ms.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        /// <returns></returns>
        public bool TryEnter(int millisecondsTimeout = 0)
        {
            System.Threading.Monitor.TryEnter(_currentObject, millisecondsTimeout, ref IsLockOwnedFlag);
            return IsLockOwnedFlag;
        }

        /// <summary>
        /// Release the current locker object.
        /// </summary>
        public void Exit()
        {
            if (IsLockOwnedFlag)
            {
                System.Threading.Monitor.Exit(_currentObject);
                IsLockOwnedFlag = false;
            }
        }

        public override void Dispose()
        {
            Exit();
        }
    }
}
