using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class WeibullDistribution : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _shape;
        private readonly double _scale;


        public WeibullDistribution() : base()
        {
        }

        public WeibullDistribution(double shape, double scale) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
        }

        public WeibullDistribution(IRandomGenerationAlgorithm randomGenerator, double shape, double scale) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
        }

        protected override T NextValue<T>()
        {
            var u = _randomGenerator.NextDouble();

            return GenericNumber<T>.FromDouble(_scale * Math.Pow(-Math.Log(u), 1 / _shape));
        }
    }
}