using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker Monitor builder
    /// </summary>
    public class MonitorBuilder : IBuilder<Monitor>
    {
        private string _name = string.Empty;
        private Context.Monitor _context = null;

        /// <summary>
        /// Build an instance of Monitor class
        /// </summary>
        /// <returns></returns>
        public Monitor Build()
        {
            return new Monitor(_context, _name);
        }

        /// <summary>
        /// Specifies a name for the new Monitor
        /// </summary>
        /// <param name="value">Specify a name for the new Monitor, name could be empty</param>
        /// <returns></returns>
        public MonitorBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        /// <summary>
        /// Specifies the context boundary for the new Monitor
        /// </summary>
        /// <param name="value">An instance of Monitor context</param>
        /// <returns></returns>
        public MonitorBuilder Context(Context.Monitor value)
        {
            this._context = value;
            return this;
        }
    }
}
