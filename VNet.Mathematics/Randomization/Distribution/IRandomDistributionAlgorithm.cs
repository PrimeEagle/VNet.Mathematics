namespace VNet.Mathematics.Randomization.Distribution
{
    public interface IRandomDistributionAlgorithm
    {
        public int Next();
        public float NextSingle();
        public double NextDouble();
        public long NextInt64();
    }
}