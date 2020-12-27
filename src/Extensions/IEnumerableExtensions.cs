using System.Collections.Generic;
using TaleWorlds.Core;

namespace TournamentsEnhanced
{
    static class IListExtenIEnumerableExtensionssions
    {
        public static List<T> AllExcept<T>(this IEnumerable<T> enumerable, T objectToExclude)
        {
            var result = new List<T>(enumerable);
            result.Remove(objectToExclude);

            return result;

        }
    }
}
