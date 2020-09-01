namespace DynaLock
{
    /// <summary>
    /// DynaLocker Semaphore builder
    /// </summary>
    public class SemaphoreBuilder
    {
        private string _name = string.Empty;
        private Context.Semaphore _context = null;
        private int _initialCount = 1;
        private int _maximumCount = 1;

        /// <summary>
        /// Build an instance of Semaphore class
        /// </summary>
        /// <returns></returns>
        public Semaphore Build()
        {
            return new Semaphore(_context, _name, _initialCount, _maximumCount);
        }

        /// <summary>
        /// Specifies a name for the new Semaphore
        /// </summary>
        /// <param name="value">Specify a name for the new Semaphore, name could be empty</param>
        /// <returns></returns>
        public SemaphoreBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        /// <summary>
        /// Specifies the context boundary for the new Semaphore
        /// </summary>
        /// <param name="value">An instance of Semaphore context</param>
        /// <returns></returns>
        public SemaphoreBuilder Context(Context.Semaphore value)
        {
            this._context = value;
            return this;
        }

        /// <summary>
        /// The initial number of requests for the semaphore that can be granted concurrently.
        /// </summary>
        /// <param name="value">An integer</param>
        /// <returns></returns>
        public SemaphoreBuilder InitialCount(int value)
        {
            this._initialCount = value;
            return this;
        }

        /// <summary>
        /// The maximum number of requests for the semaphore that can be granted concurrently.
        /// </summary>
        /// <param name="value">An integer</param>
        /// <returns></returns>
        public SemaphoreBuilder MaximumCount(int value)
        {
            this._maximumCount = value;
            return this;
        }
    }
}
