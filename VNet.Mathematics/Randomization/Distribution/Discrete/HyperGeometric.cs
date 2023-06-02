using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete
{
    public class HyperGeometric : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
    {
        private int _numberOfItems;
        private int _numberOfSuccessStates;
        private readonly int _numberOfDraws;


        public HyperGeometric(int numberOfItems, int numberOfSuccessStates, int numberOfDraws) : base()
        {
            _numberOfItems = numberOfItems;
            _numberOfSuccessStates = numberOfSuccessStates;
            _numberOfDraws = numberOfDraws;
        }

        public HyperGeometric(IRandomGenerationAlgorithm randomGenerator, int numberOfItems, int numberOfSuccessStates, int numberOfDraws)
        {
            _numberOfItems = numberOfItems;
            _numberOfSuccessStates = numberOfSuccessStates;
            _numberOfDraws = numberOfDraws;
        }

        protected override T NextValue<T>()
        {
            var success = 0;

            for (var i = 0; i < _numberOfDraws; i++)
            {
                var p = (double)_numberOfSuccessStates / _numberOfItems;
                if (_randomGenerator.Next() < p)
                {
                    success++;
                    _numberOfSuccessStates--;
                }
                _numberOfItems--;
            }

            return GenericNumber<T>.FromDouble(success);
        }
    }
}