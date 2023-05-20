using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Chi : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly int _degreesOfFreedom;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public Chi(int degreesOfFreedom) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new Gamma(_randomGenerator);
        }

        public Chi(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom) : base(randomGenerator)
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public Chi(int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _degreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = gammaDistribution;
        }

        public Chi(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
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