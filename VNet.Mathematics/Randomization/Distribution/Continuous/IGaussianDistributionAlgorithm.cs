namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public interface IGaussianDistributionAlgorithm : IContinuousRandomDistributionAlgorithm
    {
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }
    }
}