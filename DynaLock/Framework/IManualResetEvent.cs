namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's ManualResetEvent interface
    /// </summary>
    public interface IManualResetEvent
    {
        /// <summary>
        /// Blocks the current thread until current object receives a signal
        /// </summary>
        bool WaitOne();
        /// <summary>
        /// Blocks the current thread until current object receives a signal, waits maximum millisecondsTimeout ms
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        /// <returns></returns>
        bool WaitOne(int millisecondsTimeout);
        /// <summary>
        /// Release the current locker object and sends a signal
        /// </summary>
        void Set();

        /// <summary>
        /// Resets the current object to be ready to be use
        /// </summary>
        void Reset();
    }
}
