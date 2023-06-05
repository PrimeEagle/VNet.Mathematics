using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class GaussianDistribution : RandomDistributionBase, IGaussianDistributionAlgorithm
    {
        private double _mean;
        private double _standardDeviation;


        public double Mean
        {
            get => _mean;
            set
            {
                if (value < 0d) throw new ArgumentOutOfRangeException(nameof(Mean), "Must be a positive number.");
                _mean = value;
            }
        }

        public double StandardDeviation
        {
            get => _standardDeviation;
            set
            {
                if (value < 0d) throw new ArgumentOutOfRangeException(nameof(StandardDeviation), "Must be a positive number.");
                _standardDeviation = value;
            }
        }

        public GaussianDistribution(double mean, double standardDeviation) : base()
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
        }

        public GaussianDistribution(IRandomGenerationAlgorithm randomGenerator, double mean, double standardDeviation) : base(randomGenerator)
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
        }

        public GaussianDistribution(IRandomGenerationAlgorithm randomGenerator) : base(randomGenerator)
        {
        }

        public GaussianDistribution() : base()
        {
        }


        protected override T NextValue<T>()
        {
            double u1 = _randomGenerator.NextDouble();
            double u2 = _randomGenerator.NextDouble();

            // use the Box-Muller transform to get a standard normal random variable
            double standardNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            // scale and shift to get a Gaussian(random) variable with the given mean and standard deviation
            return GenericNumber<T>.FromDouble(_mean + _standardDeviation * standardNormal);
        }
    }
}