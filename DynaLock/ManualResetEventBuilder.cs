﻿using DynaLock.Framework;

namespace DynaLock
{
    /// <summary>
    /// DynaLocker ManualResetEvent builder
    /// </summary>
    public class ManualResetEventBuilder : IBuilder<ManualResetEvent>
    {
        private string _name = string.Empty;
        private Context.ManualResetEvent _context = null;

        /// <summary>
        /// Build an instance of ManualResetEvent class
        /// </summary>
        /// <returns></returns>
        public ManualResetEvent Build()
        {
            return new ManualResetEvent(_context, _name);
        }

        /// <summary>
        /// Specifies a name for the new ManualResetEvent
        /// </summary>
        /// <param name="value">Specify a name for the new ManualResetEvent, name could be empty</param>
        /// <returns></returns>
        public ManualResetEventBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        /// <summary>
        /// Specifies the context boundary for the new ManualResetEvent
        /// </summary>
        /// <param name="value">An instance of ManualResetEvent context</param>
        /// <returns></returns>
        public ManualResetEventBuilder Context(Context.ManualResetEvent value)
        {
            this._context = value;
            return this;
        }
    }
}
