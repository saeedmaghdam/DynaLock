using DynaLock.Framework;
using System.Threading;
using System.Threading.Tasks;
using SystemSemaphoreSlim = System.Threading.SemaphoreSlim;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker Monitor to create and manage semaphoreSlims dynamically in run-time
    /// </summary>
    public class SemaphoreSlim : DynaLocker<SystemSemaphoreSlim>, ISemaphoreSlim
    {
        private static IContext<SystemSemaphoreSlim> _defaultContext = new Context.Context<SystemSemaphoreSlim>();
        private readonly SystemSemaphoreSlim _currentObject;

        /// <summary>
        /// Constructor of SemaphoreSlim class
        /// </summary>
        /// <param name="context">Specify a context to have different contexts in different domains</param>
        /// <param name="name">Name of the new SemaphoreSlim</param>
        /// <param name="initialCount">The initial number of requests for the semaphoreSlim that can be granted concurrently.</param>
        /// <param name="maximumCount">The maximum number of requests for the semaphoreSlim that can be granted concurrently.</param>
        public SemaphoreSlim(Context.Context<SystemSemaphoreSlim> context, string name, int initialCount, int maximumCount) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var tempSemaphoreSlim))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out tempSemaphoreSlim))
                    {
                        _currentObject = new System.Threading.SemaphoreSlim(initialCount, maximumCount);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (tempSemaphoreSlim != null)
                _currentObject = tempSemaphoreSlim;
        }

        /// <summary>
        /// Blocks the current thread until the current object receives a signal.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        /// /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns a task</returns>
        public async Task<bool> WaitAsync(int millisecondsTimeout = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (await _currentObject.WaitAsync(millisecondsTimeout, cancellationToken))
                IsLockOwnedFlag = true;

            return IsLockOwnedFlag;
        }

        /// <summary>
        /// Release the current semaphoreSlim and send a signal to others.
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
        /// Dispose the current SemaphoreSlim
        /// </summary>
        public override void Dispose()
        {
            Release();
        }
    }
}
