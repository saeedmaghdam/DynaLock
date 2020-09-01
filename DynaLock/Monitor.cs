using DynaLock.Framework;

namespace DynaLock
{
    public class Monitor : DynaLocker, IMonitor
    {
        private static IContext _defaultContext = new Context.Monitor();
        private readonly object _currentObject;

        public Monitor(Context.Monitor context, string name) : base(context)
        {
            ContextMapper = ctx => ctx ?? _defaultContext;

            if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out _currentObject))
            {
                lock (ContextMapper.Invoke(context).LockerObject)
                {
                    if (!ContextMapper.Invoke(context).ObjectDictionary.TryGetValue(name, out _currentObject))
                    {
                        _currentObject = new object();
                        ContextMapper.Invoke(context).ObjectDictionary.TryAdd(name, _currentObject);
                    }
                }
            }
        }

        public void Enter()
        {
            System.Threading.Monitor.Enter(_currentObject, ref _isLockOwned);
        }

        public bool TryEnter(int millisecondsTimeout = 0)
        {
            System.Threading.Monitor.TryEnter(_currentObject, millisecondsTimeout, ref _isLockOwned);
            return _isLockOwned;
        }

        public void Exit()
        {
            if (_isLockOwned)
                System.Threading.Monitor.Exit(_currentObject);
        }

        public override void Dispose()
        {
            Exit();
        }
    }
}
