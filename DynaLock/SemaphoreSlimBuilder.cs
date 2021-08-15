namespace DynaLock
{
    /// <summary>
    /// DynaLocker SemaphoreSlim builder
    /// </summary>
    public class SemaphoreSlimBuilder
    {
        private string _name = string.Empty;
        private Context.SemaphoreSlim _context = null;
        private int _initialCount = 1;
        private int _maximumCount = 1;

        /// <summary>
        /// Build an instance of SemaphoreSlim class
        /// </summary>
        /// <returns></returns>
        public SemaphoreSlim Build()
        {
            return new SemaphoreSlim(_context, _name, _initialCount, _maximumCount);
        }

        /// <summary>
        /// Specifies a name for the new SemaphoreSlim
        /// </summary>
        /// <param name="value">Specify a name for the new SemaphoreSlim, name could be empty</param>
        /// <returns></returns>
        public SemaphoreSlimBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        /// <summary>
        /// Specifies the context boundary for the new SemaphoreSlim
        /// </summary>
        /// <param name="value">An instance of SemaphoreSlim context</param>
        /// <returns></returns>
        public SemaphoreSlimBuilder Context(Context.SemaphoreSlim value)
        {
            this._context = value;
            return this;
        }

        /// <summary>
        /// The initial number of requests for the semaphoreSlim that can be granted concurrently.
        /// </summary>
        /// <param name="value">An integer</param>
        /// <returns></returns>
        public SemaphoreSlimBuilder InitialCount(int value)
        {
            this._initialCount = value;
            return this;
        }

        /// <summary>
        /// The maximum number of requests for the semaphoreSlim that can be granted concurrently.
        /// </summary>
        /// <param name="value">An integer</param>
        /// <returns></returns>
        public SemaphoreSlimBuilder MaximumCount(int value)
        {
            this._maximumCount = value;
            return this;
        }
    }
}
