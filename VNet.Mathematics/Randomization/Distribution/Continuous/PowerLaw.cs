using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class PowerLaw : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _exponent;


        public PowerLaw() : base()
        {
        }

        public PowerLaw(double exponent, double scale) : base()
        {
            if (exponent < 0d) throw new ArgumentOutOfRangeException(nameof(exponent), "Must be a positive number.");

            _exponent = exponent;
        }

        public PowerLaw(IRandomGenerationAlgorithm randomGenerator, double exponent, double scale) : base(randomGenerator)
        {
            if (exponent < 0d) throw new ArgumentOutOfRangeException(nameof(exponent), "Must be a positive number.");

            _exponent = exponent;
        }

        protected override T NextValue<T>()
        {
            double u = _randomGenerator.Next();

            return GenericNumber<T>.FromDouble(Math.Pow(1 - u, -1 / _exponent));
        }
    }
}