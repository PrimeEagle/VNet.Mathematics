//namespace VNet.Mathematics.Combinatronic;

//public class Permutation<T> : ICombinatronicAlgorithm<T>
//                              where T : notnull
//{
//    public IEnumerable<IEnumerable<T>> Find(ICombinatronicAlgorithmArgs<T> args)
//    {
//        var result = new List<List<T>>();
//        if (Args.NumberPerCombination == 0) Args.NumberPerCombination = Args.List.Count;
//        Recurse<T>(Args.List, Args.NumberPerCombination, Args.WithRepetition, new List<T>(), new HashSet<int>(), result);

//        return result;
//    }

//    private static void Recurse<TInner>(IReadOnlyList<TInner> list, int depth, bool withRepetition, IList<TInner> prefix, ISet<int> prefixIndices, ICollection<List<TInner>> result)
//    {
//        if (prefix.Count == depth)
//        {
//            result.Add(new List<TInner>(prefix));
//            return;
//        }

//        for (var j = 0; j < list.Count; j++)
//        {
//            if (prefixIndices.Contains(j) && !withRepetition) continue;

//            prefix.Add(list[j]);
//            prefixIndices.Add(j);

//            Recurse<TInner>(list, depth, withRepetition, prefix, prefixIndices, result);

//            prefix.RemoveAt(prefix.Count - 1);
//            prefixIndices.Remove(j);
//        }
//    }
//}