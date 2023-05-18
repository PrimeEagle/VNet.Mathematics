namespace VNet.Mathematics.Combinatronic
{
    public interface ICombinatronicAlgorithm
    {
        public IEnumerable<IEnumerable<object>> Find(IReadOnlyList<object> collection, int numberPerCombination = 0, bool withRepetition = false);
    }

    public interface ICombinatronicAlgorithm<T> : ICombinatronicAlgorithm
    {
        public new IEnumerable<IEnumerable<T>> Find(IReadOnlyList<T> collection, int numberPerCombination = 0, bool withRepetition = false);
    }
}