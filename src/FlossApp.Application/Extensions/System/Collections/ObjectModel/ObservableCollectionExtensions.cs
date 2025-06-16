using System.Collections.ObjectModel;

namespace FlossApp.Application.Extensions.System.Collections.ObjectModel;

internal static class ObservableCollectionExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> newRange)
    {
        foreach (T item in newRange)
        {
            source.Add(item);
        }
    }

    public static async Task AddRangeAsync<T>(this ObservableCollection<T> source, IEnumerable<T> newRange)
    {
        using IEnumerator<T> enumerator = newRange.GetEnumerator();
        while (enumerator.MoveNext())
        {
            source.Add(enumerator.Current);
            await Task.Yield();
        }
    }

    public static void ClearIncrementally<T>(this ObservableCollection<T> source)
    {
        while (source.Count > 0)
        {
            source.RemoveAt(0);
        }
    }

    public static async Task ClearIncrementallyAsync<T>(this ObservableCollection<T> source)
    {
        while (source.Count > 0)
        {
            source.RemoveAt(0);
            await Task.Yield();
        }
    }

    public static void ReplaceRange<T>(this ObservableCollection<T> source, IEnumerable<T> newRange, bool useIncrementalClear = false)
    {
        if (useIncrementalClear)
        {
            source.ClearIncrementally();
        }
        else
        {
            source.Clear();
        }

        source.AddRange(newRange);
    }

    public static async Task ReplaceRangeAsync<T>(this ObservableCollection<T> source, IEnumerable<T> newCollection, bool useIncrementalClear = false)
    {
        if (useIncrementalClear)
        {
            await source.ClearIncrementallyAsync();
        }
        else
        {
            source.Clear();
        }

        await source.AddRangeAsync(newCollection);
    }
}
