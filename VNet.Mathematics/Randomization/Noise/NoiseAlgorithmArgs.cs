using VNet.Mathematics.Filter;
using VNet.Mathematics.Randomization.Distribution;

namespace VNet.Mathematics.Randomization.Noise
{
    public class NoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int QuantizeLevels { get; set; }
        public double Scale { get; set; }
        public IRandomDistributionAlgorithm RandomDistributionAlgorithm { get; set; }
        public IFilter? Filter { get; set; }
        public IFilterArgs? FilterArgs { get; set; }


        public NoiseAlgorithmArgs(IRandomDistributionAlgorithm distributionAlgorithm)
        {
            RandomDistributionAlgorithm = distributionAlgorithm;
            QuantizeLevels = 0;
            Scale = 1.0d;
        }
    }
}
