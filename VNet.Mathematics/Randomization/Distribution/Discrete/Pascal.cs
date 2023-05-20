using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class Pascal : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly double _probabilityOfSuccess;
        private readonly uint _numberOfSuccesses;

        public Pascal(double probabilityOfSuccess, uint numberOfSuccesses) : base()
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfSuccesses <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfSuccesses), "Must be a positive integer.");

            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfSuccesses = numberOfSuccesses;
        }

        public Pascal(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess, uint numberOfSuccesses) : base(randomGenerator)
        {
            if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
            if (numberOfSuccesses <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfSuccesses), "Must be a positive integer.");

            _probabilityOfSuccess = probabilityOfSuccess;
            _numberOfSuccesses = numberOfSuccesses;
        }

        protected override T NextValue<T>()
        {
            var trials = 0;
            var successes = 0;

            while (successes < _numberOfSuccesses)
            {
                trials++;
                if (_randomGenerator.Next() < _probabilityOfSuccess)
                    successes++;
            }
            return GenericNumber<T>.FromDouble(trials);
        }
    }
}