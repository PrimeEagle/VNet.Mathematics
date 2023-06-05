namespace VNet.Mathematics.Combinatronic;

public class Combination<T> : ICombinatronicAlgorithm<T>
                              where T : notnull
{
    public IEnumerable<IEnumerable<T>> Find(ICombinatronicAlgorithmArgs<T> args)
    {
        var result = new List<List<T>>();
        if (Args.NumberPerCombination == 0) Args.NumberPerCombination = Args.List.Count;
        Recurse<T>(Args.List, Args.NumberPerCombination, Args.WithRepetition, 0, new List<T>(), new HashSet<int>(), result);

        return result;
    }

    private static void Recurse<TInner>(IReadOnlyList<TInner> list, int numberPerCombination, bool withRepetition,
                                    int depth, IList<TInner> prefix, HashSet<int> prefixIndices, ICollection<List<TInner>> result)
    {
        if (prefixIndices == null) throw new ArgumentNullException(nameof(prefixIndices));
        if (prefix.Count == numberPerCombination)
        {
            result.Add(new List<TInner>(prefix));
            return;
        }

        for (var j = depth; j < list.Count; j++)
        {
            if (prefixIndices.Contains(j) && !withRepetition) continue;

            prefix.Add(list[j]);
            prefixIndices.Add(j);

            Recurse<TInner>(list, numberPerCombination, withRepetition, j, prefix, prefixIndices, result);

            prefix.RemoveAt(prefix.Count - 1);
            prefixIndices.Remove(j);
        }
    }
}