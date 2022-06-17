using System.Collections.Generic;
using UnityDeveloperKit.Runtime.Api;
using UnityEngine;

namespace UnityDeveloperKit.Runtime.Extension
{
    public static class ApiExtension
    {
        public static bool IsNull(this IObject self)
        {
            if (self is UnityEngine.Object obj)
            {
                return !obj;
            }

            return object.ReferenceEquals(self, null);
        }

    }

    public static class UnityExtension
    {
        public static bool IsNull(this UnityEngine.Object self)
        {
            return self;
        }
    }

    public static class CollectionExtension
    {
        /// <summary>
        /// 向数组中添加unity object，在对象存在时才可以成功添加
        /// </summary>
        /// <param name="collect"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddUnityObject<T>(this List<T> collect, T element) where  T : UnityEngine.Object
        {
            if (element)
            {
                if (!collect.Contains(element))
                {
                    collect.Add(element);
                }
            }
        }
        
        /// <summary>
        /// 向数组中添加object，在对象存在或者不为null时才可以成功添加
        /// </summary>
        /// <param name="collect"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddObject<T>(this List<T> collect, T element) where T : IObject
        {
            if (!element.IsNull())
            {
                if (!collect.Contains(element))
                {
                    collect.Add(element);
                }
            }
        }
    }
}