namespace VNet.Mathematics.Combinatronic
{
    public interface ICombinatronicAlgorithmArgs<T> : IMathematicsAlgorithmArgs
                                                      where T : notnull
    {
        public IReadOnlyList<T> List { get; set; }
        public int NumberPerCombination { get; set; }
        public bool WithRepetition { get; set; }
    }
}