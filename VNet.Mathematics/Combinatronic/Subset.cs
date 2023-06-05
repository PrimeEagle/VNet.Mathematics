namespace VNet.Mathematics.Combinatronic;

public class Subset<T> : ICombinatronicAlgorithm<T>
                              where T : notnull
{
    public IEnumerable<IEnumerable<T>> Find(ICombinatronicAlgorithmArgs<T> args)
    {
        var result = new List<List<T>>();

        Recurse<T>(Args.List, 0, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<TInner>(IReadOnlyList<TInner> list, int depth, IList<TInner> prefix, ISet<int> prefixIndices, ICollection<List<TInner>> result)
    {
        result.Add(new List<TInner>(prefix));

        for (var j = depth; j < list.Count; j++)
        {
            if (prefixIndices.Contains(j)) continue;

            prefix.Add(list[j]);
            prefixIndices.Add(j);

            Recurse<TInner>(list, j + 1, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }

    public IEnumerable<IEnumerable<object>> Find(IReadOnlyList<object> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<object>>();

        Recurse<object>(collection, 0, new List<object>(), new HashSet<int>(), result);

        return result;
    }
}