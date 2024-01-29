namespace Shared.Utilities;

public static class EnumerableExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="groupBy"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static bool HasDuplicated<T, TKey>(this IEnumerable<T> source, Func<T, TKey> groupBy)
        => source.GroupBy(groupBy).Any(g => g.Count() > 1);
}