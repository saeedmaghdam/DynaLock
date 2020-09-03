namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's Monitor interface
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// Wait until take the locker.
        /// </summary>
        void Enter();
        /// <summary>
        /// Tries to take the locker in given timeout in ms.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or Infinite (-1) to wait indefinitely.</param>
        /// <returns></returns>
        bool TryEnter(int millisecondsTimeout = 0);
        /// <summary>
        /// Release the current locker object.
        /// </summary>
        void Exit();
    }
}
