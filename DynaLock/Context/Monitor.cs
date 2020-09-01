using DynaLock.Framework;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace DynaLock.Context
{
    /// <summary>
    /// Monitor's context to have different work space in different domains
    /// </summary>
    public class Monitor : IContext
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
        /// Constructor of Monitor's context class
        /// </summary>
        public Monitor(){
            _objectDictionary = new ConcurrentDictionary<string, object>();
            _lockerObject = new object();
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
