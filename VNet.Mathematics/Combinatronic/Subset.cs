namespace VNet.Mathematics.Combinatronic;

public class Subset<TOuter> : ICombinatronicAlgorithm
{
    public IEnumerable<IEnumerable<TOuter>> Find(IReadOnlyList<TOuter> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<TOuter>>();

        Recurse<TOuter>(collection, 0, new List<TOuter>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<TInner>(IReadOnlyList<TInner> collection, int depth, IList<TInner> prefix, ISet<int> prefixIndices, ICollection<List<TInner>> result)
    {
        result.Add(new List<TInner>(prefix));

        for (var j = depth; j < collection.Count; j++)
        {
            if (prefixIndices.Contains(j)) continue;

            prefix.Add(collection[j]);
            prefixIndices.Add(j);

            Recurse<TInner>(collection, j + 1, prefix, prefixIndices, result);

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