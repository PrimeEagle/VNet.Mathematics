using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class ChiDistribution : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly int _degreesOfFreedom;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public ChiDistribution(int degreesOfFreedom) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new GammaDistribution(_randomGenerator);
        }

        public ChiDistribution(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom) : base(randomGenerator)
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new GammaDistribution(randomGenerator);
        }

        public ChiDistribution(int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = gammaDistribution;
        }

        public ChiDistribution(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = _degreesOfFreedom / 2.0d;
            _gammaDistribution.Scale = 2.0;

            return GenericNumber<T>.FromDouble(Math.Sqrt(_gammaDistribution.NextDouble()));
        }
    }
}