using UnityDeveloperKit.Runtime.Api;

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
}