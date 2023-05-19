﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Weibull : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _shape;
        private readonly double _scale;


        public Weibull() : base()
        {
        }

        public Weibull(double shape, double scale) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
        }

        public Weibull(IRandomGenerationAlgorithm randomGenerator, double shape, double scale) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            _shape = shape;
            _scale = scale;
        }

        protected override T NextValue<T>()
        {
            var u = _randomGenerator.NextDouble();

            return Generic.ConvertFromObject<T>(_scale * Math.Pow(-Math.Log(u), 1 / _shape));
        }
    }
}