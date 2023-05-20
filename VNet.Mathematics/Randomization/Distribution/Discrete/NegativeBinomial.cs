using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class NegativeBinomial : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly double _probabilityOfSuccess;
        private readonly uint _numberOfSuccesses;

        public NegativeBinomial(double probabilityOfSuccess, uint numberOfSuccesses) : base()
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfSuccesses <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfSuccesses), "Must be a positive integer.");

            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfSuccesses = numberOfSuccesses;
        }

        public NegativeBinomial(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess, uint numberOfSuccesses) : base(randomGenerator)
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfSuccesses <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfSuccesses), "Must be a positive integer.");

            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfSuccesses = numberOfSuccesses;
        }

        protected override T NextValue<T>()
        {
            var failures = 0;
            var successes = 0;

            while (successes < _numberOfSuccesses)
            {
                if (_randomGenerator.Next() < _probabilityOfSuccess)
                    successes++;
                else
                    failures++;
            }
            return GenericNumber<T>.FromDouble(failures);
        }
    }
}