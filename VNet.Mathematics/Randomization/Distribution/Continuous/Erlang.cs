using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class Erlang : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _shape;
        private readonly double _rate;
        private readonly IGammaDistributionAlgorithm _gammaDistribution;

        public Erlang(double shape, double rate) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (rate < 0d) throw new ArgumentOutOfRangeException(nameof(rate), "Must be a positive number.");

            _shape = shape;
            _rate = rate;
            _gammaDistribution = new Gamma();
        }

        public Erlang(IRandomGenerationAlgorithm randomGenerator, double shape, double rate) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (rate < 0d) throw new ArgumentOutOfRangeException(nameof(rate), "Must be a positive number.");

            _shape = shape;
            _rate = rate;
            _gammaDistribution = new Gamma(randomGenerator);
        }

        public Erlang(double shape, double rate, IGammaDistributionAlgorithm gammaDistribution) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (rate < 0d) throw new ArgumentOutOfRangeException(nameof(rate), "Must be a positive number.");

            _shape = shape;
            _rate = rate;
            _gammaDistribution = gammaDistribution;
        }

        public Erlang(IRandomGenerationAlgorithm randomGenerator, double shape, double rate, IGammaDistributionAlgorithm gammaDistribution) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (rate < 0d) throw new ArgumentOutOfRangeException(nameof(rate), "Must be a positive number.");

            _shape = shape;
            _rate = rate;
            _gammaDistribution = gammaDistribution;
        }

        protected override T NextValue<T>()
        {
            _gammaDistribution.Shape = _shape;
            _gammaDistribution.Scale = 1.0d / _rate;

            return Generic.ConvertFromObject<T>(_gammaDistribution.NextDouble());
        }
    }
}