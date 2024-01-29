namespace Shared.Utilities;

public static class CollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool NotNullOrEmpty<T>(this IEnumerable<T> list)
        => list != null && list.Any();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortFields"></param>
    /// <param name="sortKey"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Func<IQueryable<T>, IOrderedQueryable<T>> GetSortType<T>(
        this Dictionary<string, Func<IQueryable<T>, IOrderedQueryable<T>>> sortFields, string sortKey)
    {
        if (!sortFields.NotNullOrEmpty() || !sortFields.ContainsKey(sortKey))
            return null;

        return sortFields[sortKey];
    }
}