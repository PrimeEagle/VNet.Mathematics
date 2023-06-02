using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Uniform : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _lowerBound;
        private readonly double _upperBound;


        public Uniform() : base()
        {
        }

        public Uniform(double lowerBound, double upperBound) : base()
        {
            if (upperBound <= lowerBound) throw new ArgumentOutOfRangeException(nameof(upperBound), "upperBound must be greater than lowerBound.");

            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        public Uniform(IRandomGenerationAlgorithm randomGenerator, double lowerBound, double upperBound) : base(randomGenerator)
        {
            if (upperBound <= lowerBound) throw new ArgumentOutOfRangeException(nameof(upperBound), "upperBound must be greater than lowerBound.");

            _lowerBound = lowerBound;
            _upperBound = upperBound;
        }

        protected override T NextValue<T>()
        {
            var u = _randomGenerator.NextDouble() - 0.5d;

            return GenericNumber<T>.FromDouble(_lowerBound + (_upperBound - _lowerBound) * _randomGenerator.NextDouble());
        }
    }
}