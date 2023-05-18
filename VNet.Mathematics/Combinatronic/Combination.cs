namespace VNet.Mathematics.Combinatronic;

public class Combination<TOuter> : ICombinatronicAlgorithm
{
    public IEnumerable<IEnumerable<TOuter>> Find<TOuterT>(IReadOnlyList<TOuter> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<TOuter>>();
        if (numberPerCombination == 0) numberPerCombination = collection.Count;
        Recurse<TOuter>(collection, numberPerCombination, withRepetition, 0, new List<TOuter>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<TInner>(IReadOnlyList<TInner> collection, int numberPerCombination, bool withRepetition,
                                    int depth, IList<TInner> prefix, HashSet<int> prefixIndices, ICollection<List<TInner>> result)
    {
        if (prefixIndices == null) throw new ArgumentNullException(nameof(prefixIndices));
        if (prefix.Count == numberPerCombination)
        {
            result.Add(new List<TInner>(prefix));
            return;
        }

        for (var j = depth; j < collection.Count; j++)
        {
            if (prefixIndices.Contains(j) && !withRepetition) continue;

            prefix.Add(collection[j]);
            prefixIndices.Add(j);

            Recurse<TInner>(collection, numberPerCombination, withRepetition, j, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }

    public IEnumerable<IEnumerable<object>> Find(IReadOnlyList<object> collection, int numberPerCombination = 0, bool withRepetition = false)
    {
        var result = new List<List<object>>();
        if (numberPerCombination == 0) numberPerCombination = collection.Count;
        Recurse<object>(collection, numberPerCombination, withRepetition, 0, new List<object>(), new HashSet<int>(), result);

        return result;
    }
}