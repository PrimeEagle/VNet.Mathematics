﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class Zipf : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly int _numberOfElements;
        private readonly double _skew;
        private readonly double[] _distribution;

        public Zipf(int numberOfElements, double skew) : base()
        {
            if (_numberOfElements < 1) throw new ArgumentOutOfRangeException(nameof(numberOfElements), "Must be positive.");
            if (skew <= 0) throw new ArgumentOutOfRangeException(nameof(skew), "Must be positive.");

            _numberOfElements = numberOfElements;
            _skew = skew;
            _distribution = new double[_numberOfElements];
            InitializeDistribution();
        }

        public Zipf(IRandomGenerationAlgorithm randomGenerator, int numberOfElements, double skew) : base(randomGenerator)
        {
            if (_numberOfElements < 1) throw new ArgumentOutOfRangeException(nameof(numberOfElements), "Must be positive.");
            if (skew <= 0) throw new ArgumentOutOfRangeException(nameof(skew), "Must be positive.");

            _numberOfElements = numberOfElements;
            _skew = skew;
            _distribution = new double[_numberOfElements];
            InitializeDistribution();
        }

        private void InitializeDistribution()
        {
            double sum = 0;
            for (int i = 1; i <= _numberOfElements; i++)
            {
                sum += 1 / Math.Pow(i, _skew);
            }
            for (int i = 1; i <= _numberOfElements; i++)
            {
                _distribution[i - 1] = 1 / Math.Pow(i, _skew) / sum;
            }
        }

        protected override T NextValue<T>()
        {
            var u = _randomGenerator.NextDouble();
            var sum = 0d;

            for (var i = 0; i < _distribution.Length; i++)
            {
                sum += _distribution[i];
                if (u <= sum)
                {
                    return Generic.ConvertFromObject<T>(i + 1);
                }
            }

            return Generic.ConvertFromObject<T>(_numberOfElements);
        }
    }
}