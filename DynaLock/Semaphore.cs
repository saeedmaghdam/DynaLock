using DynaLock.Framework;

namespace DynaLock
{
    public class Semaphore : DynaLocker, ISemaphore
    {
        private static IContext _defaultContext = new Context.Semaphore();
        private readonly System.Threading.Semaphore _currentObject;

        public Semaphore(Context.Semaphore context, string name, int initialCount, int maximumCount) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out var tempSemaphore))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out tempSemaphore))
                    {
                        _currentObject = new System.Threading.Semaphore(initialCount, maximumCount, name);
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }

            if (tempSemaphore != null)
                _currentObject = (System.Threading.Semaphore)tempSemaphore;
        }

        public void WaitOne(int millisecondsTimeout = 0)
        {
            if (_currentObject.WaitOne(millisecondsTimeout))
                _isLockOwned = true;
        }

        public void Release()
        {
            if (_isLockOwned)
                _currentObject.Release();
        }

        public override void Dispose()
        {
            Release();
        }
    }
}
