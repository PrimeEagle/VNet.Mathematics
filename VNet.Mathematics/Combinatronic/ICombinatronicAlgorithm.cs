namespace VNet.Mathematics.Combinatronic
{
    public interface ICombinatronicAlgorithm<T> : IMathematicsAlgorithm
                                                  where T : notnull
    {
        public new IEnumerable<IEnumerable<T>> Find(ICombinatronicAlgorithmArgs<T> args);
    }
}