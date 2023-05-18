namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public interface IGammaDistributionAlgorithm : IContinuousRandomDistributionAlgorithm
    {
        public double Shape { get; set; }
        public double Scale { get; set; }
    }
}