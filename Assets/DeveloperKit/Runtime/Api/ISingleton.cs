namespace UnityDeveloperKit.Runtime.Api
{
    /// <summary>
    /// 单例接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton<T>
    {
        ISingleton<T> Singleton { get; }
    }
}