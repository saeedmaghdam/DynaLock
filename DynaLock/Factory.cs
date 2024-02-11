namespace DynaLock
{
    public class Factory
    {
        //public MonitorBuilder MonitorBuilder() => new MonitorBuilder();
        public SemaphoreBuilder SemaphoreBuilder() => new SemaphoreBuilder();
        public AutoResetEventBuilder AutoResetEventBuilder() => new AutoResetEventBuilder();
    }
}
