using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker AutoResetEvent builder
    /// </summary>
    public class AutoResetEventBuilder : IBuilder<AutoResetEvent>
    {
        private string _name = string.Empty;
        private Context.AutoResetEvent _context = null;

        /// <summary>
        /// Build an instance of AutoResetEvent class
        /// </summary>
        /// <returns></returns>
        public AutoResetEvent Build()
        {
            return new AutoResetEvent(_context, _name);
        }

        /// <summary>
        /// Specifies a name for the new AutoResetEvent
        /// </summary>
        /// <param name="value">Specify a name for the new AutoResetEvent, name could be empty</param>
        /// <returns></returns>
        public AutoResetEventBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        /// <summary>
        /// Specifies the context boundary for the new AutoResetEvent
        /// </summary>
        /// <param name="value">An instance of AutoResetEvent context</param>
        /// <returns></returns>
        public AutoResetEventBuilder Context(Context.AutoResetEvent value)
        {
            this._context = value;
            return this;
        }
    }
}
