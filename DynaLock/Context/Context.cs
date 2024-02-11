using System.Collections.Concurrent;
using System.Collections.Generic;
using DynaLock.Framework;

namespace DynaLock.Context
{
    public class Context<TType> : IContext<TType>
    {
        private readonly ConcurrentDictionary<string, TType> _objectDictionary;
        private readonly object _lockerObject;
        private object _metaData;
        private readonly object _metaDataLocker = new object();
        private readonly object _contextLocker = new object();
        private bool _isContextLocked = false;

        private readonly IFlag _flag1;
        private readonly IFlag _flag2;
        private readonly IFlag _flag3;
        private readonly IFlag _flag4;
        private readonly IFlag _flag5;

        public IFlag Flag1 => _flag1;
        public IFlag Flag2 => _flag2;
        public IFlag Flag3 => _flag3;
        public IFlag Flag4 => _flag4;
        public IFlag Flag5 => _flag5;

        /// <summary>
        /// A dictionary to store objects required by DynaLocker
        /// </summary>
        public ConcurrentDictionary<string, TType> ObjectDictionary => _objectDictionary;

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

        public Context()
        {
            _lockerObject = new List<object>();
            _objectDictionary = new ConcurrentDictionary<string, TType>();

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
