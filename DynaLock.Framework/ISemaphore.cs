namespace DynaLock.Framework
{
    public interface ISemaphore
    {
        void WaitOne(int millisecondsTimeout = 0);
        void Release();
    }
}
