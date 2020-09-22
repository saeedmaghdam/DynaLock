using System.Collections.Concurrent;
using System.Collections.Generic;
using DynaLock.Framework;

namespace DynaLock.Context
{
    public abstract class ContextAbstract : IContext
    {
        private ConcurrentDictionary<string, object> _objectDictionary;
        private object _lockerObject;
        private object _metaData;
        private object _metaDataLocker = new object();

        /// <summary>
        /// A dictionary to store objects required by DynaLocker
        /// </summary>
        public ConcurrentDictionary<string, object> ObjectDictionary => _objectDictionary;

        /// <summary>
        /// Generic locker object used to get objects from dictionary.
        /// </summary>
        public object LockerObject => _lockerObject;

        /// <summary>
        /// An object to store meta data in current context.
        /// </summary>
        public object MetaData
        {
            get
            {
                lock (_metaDataLocker)
                    return _metaData;
            }
            set
            {
                lock (_metaDataLocker)
                    _metaData = value;
            }
        }

        protected ContextAbstract()
        {
            _lockerObject = new List<object>();
            _objectDictionary = new ConcurrentDictionary<string, object>();
        }
    }
}
