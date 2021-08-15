using System.Threading;
using System.Threading.Tasks;

namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's SemaphoreSlim interface
    /// </summary>
    public interface ISemaphoreSlim
    {
        /// <summary>
        /// Blocks the current thread until the current object receives a signal.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        /// /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns a task</returns>
        Task<bool> WaitAsync(int millisecondsTimeout = 0, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Release the current semaphoreSlim and send a signal to others.
        /// </summary>
        void Release();
    }
}
