namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's Semaphore interface
    /// </summary>
    public interface ISemaphore
    {
        /// <summary>
        /// Blocks the current thread until the current object receives a signal.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        void WaitOne(int millisecondsTimeout = 0);

        /// <summary>
        /// Release the current semaphore and send a signal to others.
        /// </summary>
        void Release();
    }
}
