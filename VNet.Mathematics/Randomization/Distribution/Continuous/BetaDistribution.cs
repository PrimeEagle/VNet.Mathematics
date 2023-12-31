﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class BetaDistribution : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _alpha;
        private readonly double _beta;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public BetaDistribution(double alpha, double beta) : base()
        {
            if (alpha < 0d) throw new ArgumentOutOfRangeException(nameof(alpha), "Must be a positive number.");
            if (beta < 0d) throw new ArgumentOutOfRangeException(nameof(beta), "Must be a positive number.");

            _alpha = alpha;
            _beta = beta;
            _gammaDistribution = new GammaDistribution();
        }

        public BetaDistribution(IRandomGenerationAlgorithm randomGenerator, double alpha, double beta) : base(randomGenerator)
        {
            if (alpha < 0d) throw new ArgumentOutOfRangeException(nameof(alpha), "Must be a positive number.");
            if (beta < 0d) throw new ArgumentOutOfRangeException(nameof(beta), "Must be a positive number.");

            _alpha = alpha;
            _beta = beta;
            _gammaDistribution = new GammaDistribution(randomGenerator);
        }

        public BetaDistribution(double alpha, double beta, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (alpha < 0d) throw new ArgumentOutOfRangeException(nameof(alpha), "Must be a positive number.");
            if (beta < 0d) throw new ArgumentOutOfRangeException(nameof(beta), "Must be a positive number.");

            _alpha = alpha;
            _beta = beta;
            _gammaDistribution = gammaDistribution;
        }

        public BetaDistribution(IRandomGenerationAlgorithm randomGenerator, double alpha, double beta, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (alpha < 0d) throw new ArgumentOutOfRangeException(nameof(alpha), "Must be a positive number.");
            if (beta < 0d) throw new ArgumentOutOfRangeException(nameof(beta), "Must be a positive number.");

            _alpha = alpha;
            _beta = beta;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = _alpha;
            _gammaDistribution.Scale = 1.0d;
            var gamma1Value = _gammaDistribution.NextDouble();

            _gammaDistribution.Shape = _beta;
            _gammaDistribution.Scale = 1.0d;
            var gamma2Value = _gammaDistribution.NextDouble();

            return GenericNumber<T>.FromDouble(gamma1Value / (gamma1Value + gamma2Value));
        }
    }
}