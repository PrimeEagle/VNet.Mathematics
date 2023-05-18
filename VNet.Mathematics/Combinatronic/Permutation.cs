namespace VNet.Mathematics.Combinatronic;

public class Permutation<TOuter> : ICombinatronicAlgorithm
{
    public IEnumerable<IEnumerable<TOuter>> Find(IReadOnlyList<TOuter> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<TOuter>>();
        if (numberPerCombination == 0) numberPerCombination = collection.Count;
        Recurse<TOuter>(collection, numberPerCombination, withRepetition, new List<TOuter>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<TInner>(IReadOnlyList<TInner> collection, int depth, bool withRepetition, IList<TInner> prefix, ISet<int> prefixIndices, ICollection<List<TInner>> result)
    {
        if (prefix.Count == depth)
        {
            result.Add(new List<TInner>(prefix));
            return;
        }

        for (var j = 0; j < collection.Count; j++)
        {
            if (prefixIndices.Contains(j) && !withRepetition) continue;

            prefix.Add(collection[j]);
            prefixIndices.Add(j);

            Recurse<TInner>(collection, depth, withRepetition, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }

    public IEnumerable<IEnumerable<object>> Find(IReadOnlyList<object> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<object>>();
        if (numberPerCombination == 0) numberPerCombination = collection.Count;
        Recurse<object>(collection, numberPerCombination, withRepetition, new List<object>(), new HashSet<int>(), result);

        return result;
    }
}