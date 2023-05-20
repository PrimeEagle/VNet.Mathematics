using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Laplace : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _location;
        private readonly double _scale;


        public Laplace() : base()
        {
        }

        public Laplace(double location, double scale) : base()
        {
            if (location < 0d) throw new ArgumentOutOfRangeException(nameof(location), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _location = location;
            _scale = scale;
        }

        public Laplace(IRandomGenerationAlgorithm randomGenerator, double location, double scale) : base(randomGenerator)
        {
            if (location < 0d) throw new ArgumentOutOfRangeException(nameof(location), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _location = location;
            _scale = scale;
        }

        protected override T NextValue<T>()
        {
            var u = _randomGenerator.NextDouble() - 0.5d;

            return GenericNumber<T>.FromDouble(_location - _scale * Math.Sign(u) * Math.Log(1 - 2 * Math.Abs(u)));
        }
    }
}