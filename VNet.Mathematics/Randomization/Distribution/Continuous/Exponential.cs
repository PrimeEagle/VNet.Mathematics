using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Exponential : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _lambda;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public Exponential(double lambda) : base()
        {
            if (lambda <= 0d) throw new ArgumentOutOfRangeException(nameof(lambda), "Must be a positive number.");

            _lambda = lambda;
            _gammaDistribution = new Gamma();
        }

        public Exponential(IRandomGenerationAlgorithm randomGenerator, double lambda) : base(randomGenerator)
        {
            if (lambda <= 0d) throw new ArgumentOutOfRangeException(nameof(lambda), "Must be a positive number.");

            _lambda = lambda;
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public Exponential(double lambda, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (lambda <= 0d) throw new ArgumentOutOfRangeException(nameof(lambda), "Must be a positive number.");

            _lambda = lambda;
            _gammaDistribution = gammaDistribution;
        }

        public Exponential(IRandomGenerationAlgorithm randomGenerator, double lambda, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (lambda <= 0d) throw new ArgumentOutOfRangeException(nameof(lambda), "Must be a positive number.");

            _lambda = lambda;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = 1.0d;
            _gammaDistribution.Scale = 1.0d / _lambda;

            return Generic.ConvertFromObject<T>(_gammaDistribution.NextDouble());
        }
    }
}