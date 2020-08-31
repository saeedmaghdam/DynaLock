using System;
using System.Collections.Concurrent;

namespace DynaLock
{
    public class Semaphore : ISemaphore
    {
        private static readonly ConcurrentDictionary<string, System.Threading.Semaphore> _lockerDictionary = new ConcurrentDictionary<string, System.Threading.Semaphore>();
        private static readonly object GenericLockerObject = new object();
        private readonly System.Threading.Semaphore _currentSemaphore;
        private bool _isLockOwned = false;

        public Semaphore(Context.Semaphore context, string name, int initialCount, int maximumCount)
        {
            if (context != null)
            {
                if (!context.SemaphoreDictionary.TryGetValue(name, out _currentSemaphore))
                {
                    lock (context.GenericLockerObject)
                    {
                        if (!context.SemaphoreDictionary.TryGetValue(name, out _currentSemaphore))
                        {
                            _currentSemaphore = new System.Threading.Semaphore(initialCount, maximumCount, name);
                            context.SemaphoreDictionary.TryAdd(name, _currentSemaphore);
                        }
                    }
                }
            }
            else
            {
                if (!_lockerDictionary.TryGetValue(name, out _currentSemaphore))
                {
                    lock (GenericLockerObject)
                    {
                        if (!_lockerDictionary.TryGetValue(name, out _currentSemaphore))
                        {
                            _currentSemaphore = new System.Threading.Semaphore(initialCount, maximumCount, name);
                            _lockerDictionary.TryAdd(name, _currentSemaphore);
                        }
                    }
                }
            }
        }

        public bool IsLockOwned() => _isLockOwned;

        public void WaitOne(int millisecondsTimeout = 0)
        {
            if (_currentSemaphore.WaitOne(millisecondsTimeout))
                _isLockOwned = true;
        }

        public void Release()
        {
            if (_isLockOwned)
            {
                try
                {
                    _currentSemaphore.Release();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public void Dispose()
        {
            Release();
        }
    }
}
