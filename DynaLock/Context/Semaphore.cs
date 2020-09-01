using DynaLock.Framework;
using System.Collections.Concurrent;

namespace DynaLock.Context
{
    public class Semaphore : IContext
    {
        private ConcurrentDictionary<string, object> _objectDictionary;
        private object _lockerObject;
        private object _metaData;
        private object _metaDataLocker = new object();

        public ConcurrentDictionary<string, object> ObjectDictionary => _objectDictionary;
        public object LockerObject => _lockerObject;

        public Semaphore()
        {
            _objectDictionary = new ConcurrentDictionary<string, object>();
            _lockerObject = new object();
        }

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
