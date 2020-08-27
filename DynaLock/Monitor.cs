using System.Collections.Concurrent;

namespace DynaLock
{
    public class Monitor : IMonitor
    {
        private static readonly ConcurrentDictionary<string, object> _lockerDictionary = new ConcurrentDictionary<string, object>();
        private static readonly object GenericLockerObject = new object();
        private readonly object _currentLockObject;
        private bool _isLockOwned = false;

        public Monitor(string lockerName = "")
        {
            if (!_lockerDictionary.TryGetValue(lockerName, out _currentLockObject))
            {
                lock (GenericLockerObject)
                {
                    if (!_lockerDictionary.TryGetValue(lockerName, out _currentLockObject))
                    {
                        _currentLockObject = new object();
                        _lockerDictionary.TryAdd(lockerName, _currentLockObject);
                    }
                }
            }
        }

        public bool IsLockOwned() => _isLockOwned;

        public void Enter()
        {
            System.Threading.Monitor.Enter(_currentLockObject, ref _isLockOwned);
        }

        public bool TryEnter(int millisecondsTimeout = 0)
        {
            System.Threading.Monitor.TryEnter(_currentLockObject, millisecondsTimeout, ref _isLockOwned);
            return _isLockOwned;
        }

        public void Dispose()
        {
            if (_isLockOwned)
                System.Threading.Monitor.Exit(_currentLockObject);
        }
    }
}
