using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class ChiSquare : RandomDistributionBase, IChiSquareDistributionAlgorithm
    {
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public int DegreesOfFreedom { get; set; }

        public ChiSquare() : base()
        {
        }

        public ChiSquare(int degreesOfFreedom) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            DegreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new Gamma(_randomGenerator);
        }

        public ChiSquare(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom) : base(randomGenerator)
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            DegreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public ChiSquare(IRandomGenerationAlgorithm randomGenerator) : base(randomGenerator)
        {
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public ChiSquare(int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            DegreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = gammaDistribution;
        }

        public ChiSquare(IRandomGenerationAlgorithm randomGenerator, int degreesOfFreedom, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (degreesOfFreedom < 0) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            DegreesOfFreedom = degreesOfFreedom;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = DegreesOfFreedom / 2.0d;
            _gammaDistribution.Scale = 2.0;

            return GenericNumber<T>.FromDouble(_gammaDistribution.NextDouble());
        }
    }
}