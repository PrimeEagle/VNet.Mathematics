﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class LogNormalDistribution : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _mean;
        private readonly double _standardDeviation;
        private readonly IGaussianDistributionAlgorithm _gaussianDistribution;

        public LogNormalDistribution(double mean, double standardDeviation) : base()
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _gaussianDistribution = new GaussianDistribution(_randomGenerator);
        }

        public LogNormalDistribution(IRandomGenerationAlgorithm randomGenerator, double mean, double standardDeviation) : base(randomGenerator)
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _gaussianDistribution = new GaussianDistribution(randomGenerator);
        }

        public LogNormalDistribution(double mean, double standardDeviation, IGaussianDistributionAlgorithm gaussianDistribution) : base()
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _gaussianDistribution = gaussianDistribution;
        }

        public LogNormalDistribution(IRandomGenerationAlgorithm randomGenerator, double mean, double standardDeviation, IGaussianDistributionAlgorithm gaussianDistribution) : base(randomGenerator)
        {
            if (mean < 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation < 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _gaussianDistribution = gaussianDistribution;
        }

        protected override T NextValue<T>()
        {
            _gaussianDistribution.Mean = _mean;
            _gaussianDistribution.StandardDeviation = _standardDeviation;

            return GenericNumber<T>.FromDouble(Math.Exp(_gaussianDistribution.NextDouble()));
        }
    }
}