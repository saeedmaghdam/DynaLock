namespace DynaLock.Framework
{
    public interface IFlag
    {
        object Data
        {
            get;
            set;
        }

        bool IsNull
        {
            get;
        }

        bool IsLocked
        {
            get;
        }

        void SetNull();

        bool Lock();

        void Unlock();
    }
}
