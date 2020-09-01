using System.Collections.Concurrent;

namespace DynaLock.Framework
{
    public interface IContext
    {
        ConcurrentDictionary<string, object> ObjectDictionary
        {
            get;
        }

        object LockerObject
        {
            get;
        }

        object MetaData
        {
            get;
            set;
        }
    }
}
