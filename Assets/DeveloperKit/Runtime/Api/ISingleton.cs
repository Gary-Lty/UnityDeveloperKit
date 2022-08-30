namespace UnityDeveloperKit.Runtime.Api
{
    /// <summary>
    /// 单例接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISingleton<T>
    {
        public static ISingleton<T> Singleton { get; private set; }

        public sealed void Init()
        {
            Singleton = this;
        }
    }
}