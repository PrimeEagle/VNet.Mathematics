namespace VNet.Mathematics.Combinatronic
{
    public class CombinatronicAlgorithmArgs<T> : ICombinatronicAlgorithmArgs<T>
                                                 where T : notnull
    {
        public IReadOnlyList<T> List { get; set; }
        public int NumberPerCombination { get; set; }
        public bool WithRepetition { get; set; }

        public CombinatronicAlgorithmArgs()
        {
            List = new List<T>();
        }

        public CombinatronicAlgorithmArgs(IReadOnlyList<T> list)
        {
            List = list;
        }
    }
}