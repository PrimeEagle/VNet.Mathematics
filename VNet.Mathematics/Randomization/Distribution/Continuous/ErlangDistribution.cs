using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class ErlangDistribution : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _shape;
        private readonly double _scale;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public ErlangDistribution(double shape, double scale) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = new GammaDistribution(_randomGenerator);
        }

        public ErlangDistribution(IRandomGenerationAlgorithm randomGenerator, double shape, double scale) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = new GammaDistribution(randomGenerator);
        }

        public ErlangDistribution(double shape, double scale, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
            _gammaDistribution = gammaDistribution;
        }

        public ErlangDistribution(IRandomGenerationAlgorithm randomGenerator, double shape, double scale, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
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
            _gammaDistribution.Scale = 1.0d / _scale;

            return GenericNumber<T>.FromDouble(_gammaDistribution.NextDouble());
        }
    }
}