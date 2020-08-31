using System.Collections.Concurrent;

namespace DynaLock.Context
{
    public class Semaphore
    {
        private static ConcurrentDictionary<string, System.Threading.Semaphore> _semaphoreDictionary;
        private static object _genericLockerObject;

        public ConcurrentDictionary<string, System.Threading.Semaphore> SemaphoreDictionary => _semaphoreDictionary;
        public object GenericLockerObject => _genericLockerObject;

        public Semaphore(){
            _semaphoreDictionary = new ConcurrentDictionary<string, System.Threading.Semaphore>();
            _genericLockerObject = new object();
        }
    }
}
