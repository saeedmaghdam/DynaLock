namespace DynaLock.Framework
{
    /// <summary>
    /// Builder design pattern to make it easy to create instances of DynaLock classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Builds an instance of class
        /// </summary>
        /// <returns></returns>
        T Build();
    }
}
