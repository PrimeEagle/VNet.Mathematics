using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Cauchy : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _location;
        private readonly double _scale;


        public Cauchy() : base()
        {
        }

        public Cauchy(double location, double scale) : base()
        {
            if (location < 0d) throw new ArgumentOutOfRangeException(nameof(location), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _location = location;
            _scale = scale;
        }

        public Cauchy(IRandomGenerationAlgorithm randomGenerator, double location, double scale) : base(randomGenerator)
        {
            if (location < 0d) throw new ArgumentOutOfRangeException(nameof(location), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _location = location;
            _scale = scale;
        }

        protected override T NextValue<T>()
        {
            var u1 = _randomGenerator.NextDouble();
            var u2 = _randomGenerator.NextDouble();

            // generate a standard Cauchy random variable
            var standardCauchy = Math.Tan(Math.PI * (u1 - 0.5));

            // scale and shift to get a Cauchy(random) variable with the given location and scale parameters
            return GenericNumber<T>.FromDouble(_location + _scale * standardCauchy);
        }
    }
}