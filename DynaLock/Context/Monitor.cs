using System.Collections.Concurrent;

namespace DynaLock.Context
{
    public class Monitor
    {
        private static ConcurrentDictionary<string, object> _lockerDictionary;
        private static object _genericLockerObject;

        public ConcurrentDictionary<string, object> LockerDictionary => _lockerDictionary;
        public object GenericLockerObject => _genericLockerObject;

        public Monitor(){
            _lockerDictionary = new ConcurrentDictionary<string, object>();
            _genericLockerObject = new object();
        }
    }
}
