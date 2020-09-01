using System.Collections.Concurrent;

namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's context interface
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// A dictionary to store objects required by DynaLocker
        /// </summary>
        ConcurrentDictionary<string, object> ObjectDictionary
        {
            get;
        }

        /// <summary>
        /// Generic locker object used to get objects from dictionary.
        /// </summary>
        object LockerObject
        {
            get;
        }

        /// <summary>
        /// An object to store meta data in current context.
        /// </summary>
        object MetaData
        {
            get;
            set;
        }
    }
}
