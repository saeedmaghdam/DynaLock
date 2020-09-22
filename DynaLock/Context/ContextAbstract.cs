using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DynaLock.Framework;

namespace DynaLock.Context
{
    public abstract class ContextAbstract : IContext
    {
        private ConcurrentDictionary<string, object> _objectDictionary;
        private object _lockerObject;
        private object _metaData;
        private object _metaDataLocker = new object();
        private bool _isLockTakenFlag;
        private object _contextLocker = new object();
        private bool _isContextLocked = false;

        private IFlag _flag1;
        private IFlag _flag2;
        private IFlag _flag3;
        private IFlag _flag4;
        private IFlag _flag5;

        public IFlag Flag1 => _flag1;
        public IFlag Flag2 => _flag2;
        public IFlag Flag3 => _flag3;
        public IFlag Flag4 => _flag4;
        public IFlag Flag5 => _flag5;

        /// <summary>
        /// A dictionary to store objects required by DynaLocker
        /// </summary>
        public ConcurrentDictionary<string, object> ObjectDictionary => _objectDictionary;

        /// <summary>
        /// Generic locker object used to get objects from dictionary.
        /// </summary>
        public object LockerObject => _lockerObject;

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

        public bool IsMetaDataNull
        {
            get
            {
                lock (_metaDataLocker)
                    return _metaData == null;
            }
        }

        protected ContextAbstract()
        {
            _lockerObject = new List<object>();
            _objectDictionary = new ConcurrentDictionary<string, object>();

            _flag1 = new Flag();
            _flag2 = new Flag();
            _flag3 = new Flag();
            _flag4 = new Flag();
            _flag5 = new Flag();
        }

        public void SetMetaDataNull()
        {
            lock (_metaDataLocker)
                _metaData = null;
        }

        public bool Lock()
        {
            bool isContextLocked = false;
            System.Threading.Monitor.Enter(_contextLocker, ref isContextLocked);
            _isContextLocked = isContextLocked;
            return _isContextLocked;
        }

        public void Unlock()
        {
            if (_isContextLocked)
            {
                System.Threading.Monitor.Exit(_contextLocker);
                _isContextLocked = false;
            }
        }
    }
}
