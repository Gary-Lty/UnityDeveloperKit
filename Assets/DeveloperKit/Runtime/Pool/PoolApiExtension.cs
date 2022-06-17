using UnityEngine;

namespace DeveloperKit.Runtime.Pool
{
    public static class PoolApiExtension
    {
        /// <summary>
        /// 通过creator回收
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void Recycle<T>(this IHasCreator<T> self)
        {
            if (self.Creator != null)
            {
                self.Creator.RecycleItem((T)self);
            }
            else
            {
                Debug.LogError("recycle error,creator is null!");
            }
        }
    }
}