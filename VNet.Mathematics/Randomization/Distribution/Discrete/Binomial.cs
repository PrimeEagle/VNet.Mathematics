using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class Binomial : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly double _probabilityOfSuccess;
        private readonly uint _numberOfTrials;

        public Binomial(double probabilityOfSuccess, uint numberOfTrials) : base()
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfTrials <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfTrials), "Must be a positive integer.");
            
            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfTrials = numberOfTrials;
        }

        public Binomial(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess, uint numberOfTrials) : base(randomGenerator)
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfTrials <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfTrials), "Must be a positive integer.");

            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfTrials = numberOfTrials;
        }

        protected override T NextValue<T>()
        {
            var success = 0;
            for (var i = 0; i < _numberOfTrials; i++)
            {
                if (_randomGenerator.NextDouble() < _probabilityOfSuccess)
                    success++;
            }

            return Generic.ConvertFromObject<T>(success);
        }
    }
}