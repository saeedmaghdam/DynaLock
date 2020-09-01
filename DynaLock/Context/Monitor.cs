using DynaLock.Framework;
using System.Collections.Concurrent;

namespace DynaLock.Context
{
    public class Monitor : IContext
    {
        private ConcurrentDictionary<string, object> _objectDictionary;
        private object _lockerObject;

        public ConcurrentDictionary<string, object> ObjectDictionary => _objectDictionary;
        public object LockerObject => _lockerObject;

        public Monitor(){
            _objectDictionary = new ConcurrentDictionary<string, object>();
            _lockerObject = new object();
        }
    }
}
