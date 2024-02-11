using System.Collections.Concurrent;

namespace DynaLock.Framework
{
    /// <summary>
    /// DynaLock's context interface
    /// </summary>
    public interface IContext<TType>
    {
        /// <summary>
        /// A dictionary to store objects required by DynaLocker
        /// </summary>
        ConcurrentDictionary<string, TType> ObjectDictionary
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

        /// <summary>
        /// Lock context to modify meta data and etc
        /// </summary>
        /// <returns></returns>
        bool Lock();

        /// <summary>
        /// Unlock context if it's locked already
        /// </summary>
        void Unlock();
        /// <summary>
        /// Indicated whether meta data is null or not
        /// </summary>
        bool IsMetaDataNull
        {
            get;
        }
        /// <summary>
        /// Set meta data to null
        /// </summary>
        void SetMetaDataNull();

        IFlag Flag1
        {
            get;
        }

        IFlag Flag2
        {
            get;
        }

        IFlag Flag3
        {
            get;
        }

        IFlag Flag4
        {
            get;
        }

        IFlag Flag5
        {
            get;
        }
    }
}
