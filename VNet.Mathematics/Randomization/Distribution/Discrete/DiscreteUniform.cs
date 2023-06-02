﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class DiscreteUniform : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly long _minimum;
        private readonly long _maximum;


        public DiscreteUniform(long minimum, long maximum) : base()
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException(nameof(minimum), "Must be less than maximum.");

            _minimum = minimum;
            _maximum = maximum;
        }

        public DiscreteUniform(IRandomGenerationAlgorithm randomGenerator, long minimum, long maximum) : base(randomGenerator)
        {
            if (minimum > maximum) throw new ArgumentOutOfRangeException(nameof(minimum), "Must be less than maximum.");

            _minimum = minimum;
            _maximum = maximum;
        }

        protected override T NextValue<T>()
        {
            return GenericNumber<T>.FromDouble(_minimum + (_randomGenerator.NextInt64() * (_maximum - _minimum + 1)));
        }
    }
}