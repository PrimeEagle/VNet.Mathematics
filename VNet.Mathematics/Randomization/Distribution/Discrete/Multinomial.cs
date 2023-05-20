using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class Multinomial : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private readonly double[] _probabilities;
        private readonly int _numberOfTrials;
        private static long _numberOfCalls;
        private static int[]  _counts;

        public Multinomial(double[] probabilities, int numberOfTrials) : base()
        {
            var sum = 0d;
            foreach (var p in probabilities)
            {
                sum += p;
            }

            if (Math.Abs(sum - 1.0d) > 0.0000001d) throw new ArgumentOutOfRangeException(nameof(probabilities), "Sum of all probabilities must equal 1.");
            if (numberOfTrials <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfTrials), "Must be a positive integer.");

            _probabilities = probabilities;
            _numberOfTrials = numberOfTrials;
            Reset();
        }

        public Multinomial(IRandomGenerationAlgorithm randomGenerator, double[] probabilities, int numberOfTrials) : base(randomGenerator)
        {
            var sum = 0d;
            foreach (var p in probabilities)
            {
                sum += p;
            }

            if (Math.Abs(sum - 1.0d) > 0.0000001d) throw new ArgumentOutOfRangeException(nameof(probabilities), "Sum of all probabilities must equal 1.");
            if (numberOfTrials <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfTrials), "Must be a positive integer.");

            _probabilities = probabilities;
            _numberOfTrials = numberOfTrials;
            Reset();
        }

        protected override T NextValue<T>()
        {
            _numberOfCalls++;

            if (_numberOfCalls == _probabilities.Length)
            {
                Reset();
            }

            if (_numberOfCalls != 1) return GenericNumber<T>.FromDouble(_counts[_numberOfCalls - 1]);
            for (var i = 0; i < _numberOfTrials; i++)
            {
                var u = _randomGenerator.NextDouble();
                var cumulativeProbability = 0.0;

                for (var j = 0; j < _probabilities.Length; j++)
                {
                    cumulativeProbability += _probabilities[j];
                    if (!(u < cumulativeProbability)) continue;
                    _counts[j]++;
                    break;
                }
            }

            return GenericNumber<T>.FromDouble(_counts[_numberOfCalls - 1]);
        }

        public void Reset()
        {
            _numberOfCalls = 0;
            _counts = new int[_probabilities.Length];
        }
    }
}