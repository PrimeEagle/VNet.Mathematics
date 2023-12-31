﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class GeometricDistribution : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly double _probabilityOfSuccess;

        public GeometricDistribution(double probabilityOfSuccess, uint numberOfTrials) : base()
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");

            _probabilityOfSuccess = probabilityOfSuccess;
        }

        public GeometricDistribution(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess, uint numberOfTrials) : base(randomGenerator)
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");

            _probabilityOfSuccess = probabilityOfSuccess;
        }

        protected override T NextValue<T>()
        {
            double u = _randomGenerator.Next();

            return GenericNumber<T>.FromDouble(Math.Ceiling(Math.Log(1 - u) / Math.Log(1 - _probabilityOfSuccess)));
        }
    }
}