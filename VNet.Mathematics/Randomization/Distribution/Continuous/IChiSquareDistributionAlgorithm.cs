namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public interface IChiSquareDistributionAlgorithm : IContinuousRandomDistributionAlgorithm
    {
        public int DegreesOfFreedom { get; set; }
    }
}