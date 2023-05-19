using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class InverseGamma : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _shape;
        private readonly double _scale;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public InverseGamma(double shape, double scale) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = new Gamma(_randomGenerator);
        }

        public InverseGamma(IRandomGenerationAlgorithm randomGenerator, double shape, double scale) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public InverseGamma(double shape, double scale, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = gammaDistribution;
        }

        public InverseGamma(IRandomGenerationAlgorithm randomGenerator, double shape, double scale, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = _shape;
            _gammaDistribution.Scale = _scale;
            double gammaSample = _gammaDistribution.NextDouble();

            if (gammaSample == 0)
            {
                throw new Exception("Division by zero: Gamma sample cannot be zero for Inverse Gamma distribution.");
            }

            return Generic.ConvertFromObject<T>(1.0d / gammaSample);
        }
    }
}