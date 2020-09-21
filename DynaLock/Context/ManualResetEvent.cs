using DynaLock.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DynaLock.Context
{
    /// <summary>
    /// ManualResetEvent's context to have different work space in different domains
    /// </summary>
    public class ManualResetEvent : IContext
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
        /// Constructor of ManualResetEvent's context class
        /// </summary>
        public ManualResetEvent(){
            _objectDictionary = new ConcurrentDictionary<string, object>();
            _lockerObject = new List<object>();
        }

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
    }
}
