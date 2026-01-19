using System.Collections.Generic;
using System.Linq;

namespace UnityGameFramework.Runtime
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 判断集合是否有效（不为null且包含至少一个元素）
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="collection">要检查的集合</param>
        /// <returns>如果集合不为null且包含至少一个元素，则返回true；否则返回false</returns>
        public static bool IsValid<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }
    }
}