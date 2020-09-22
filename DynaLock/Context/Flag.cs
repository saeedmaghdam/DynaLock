using DynaLock.Framework;

namespace DynaLock.Context
{
    class Flag : IFlag
    {
        private object _flagLocker = new object();
        private object _innerLocker = new object();
        private object _flagData;

        private bool _isFlagLocked;

        public object Data
        {
            get
            {
                lock (_innerLocker)
                    return _flagData;
            }
            set
            {
                lock (_innerLocker)
                    _flagData = value;
            }
        }

        public bool IsNull
        {
            get
            {
                lock (_innerLocker)
                    return _flagData == null;
            }
        }

        public bool IsLocked => _isFlagLocked;

        public void SetNull()
        {
            lock (_innerLocker)
                _flagData = null;
        }

        public bool Lock()
        {
            bool isContextLocked = false;
            System.Threading.Monitor.Enter(_flagLocker, ref isContextLocked);
            _isFlagLocked = isContextLocked;
            return _isFlagLocked;
        }

        public void Unlock()
        {
            if (_isFlagLocked)
            {
                System.Threading.Monitor.Exit(_flagLocker);
                _isFlagLocked = false;
            }
        }
    }
}
