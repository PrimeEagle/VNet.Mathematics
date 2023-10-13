using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class DiscreteUniformDistribution : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly long _minimum;
        private readonly long _maximum;


        public DiscreteUniformDistribution(long minimum, long maximum) : base()
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException(nameof(minimum), "Must be less than maximum.");

            _minimum = minimum;
            _maximum = maximum;
        }

        public DiscreteUniformDistribution(IRandomGenerationAlgorithm randomGenerator, long minimum, long maximum) : base(randomGenerator)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException(nameof(minimum), "Must be less than maximum.");

            _minimum = minimum;
            _maximum = maximum;
        }

        protected override T NextValue<T>()
        {
            return GenericNumber<T>.FromDouble(_minimum + (_randomGenerator.NextLong() * (_maximum - _minimum + 1)));
        }
    }
}