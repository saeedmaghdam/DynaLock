using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker Monitor to create and manage semaphores dynamically in run-time
    /// </summary>
    public class Semaphore : DynaLocker, ISemaphore
    {
        private static IContext _defaultContext = new Context.Semaphore();
        private readonly System.Threading.Semaphore _currentObject;

        /// <summary>
        /// Constructor of Semaphore class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new Semaphore</param>
        /// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
        /// <param name="maximumCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
        public Semaphore(Context.Semaphore context, string name, int initialCount, int maximumCount) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var tempSemaphore))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out tempSemaphore))
                    {
                        _currentObject = new System.Threading.Semaphore(initialCount, maximumCount, name);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (tempSemaphore != null)
                _currentObject = (System.Threading.Semaphore)tempSemaphore;
        }

        /// <summary>
        /// Blocks the current thread until the current object receives a signal.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        public void WaitOne(int millisecondsTimeout = 0)
        {
            if (_currentObject.WaitOne(millisecondsTimeout))
                IsLockOwnedFlag = true;
        }

        /// <summary>
        /// Release the current semaphore and send a signal to others.
        /// </summary>
        public void Release()
        {
            if (IsLockOwnedFlag)
            {
                _currentObject.Release();
                IsLockOwnedFlag = false;
            }
        }

        /// <summary>
        /// Dispose the current Semaphore
        /// </summary>
        public override void Dispose()
        {
            Release();
        }
    }
}
