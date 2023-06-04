using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class ButterworthNotchFilter : FilterBase
    {
        public ButterworthNotchFilter(IButterworthNotchFilterArgs args) : base(args)
        {
            Algorithm = new ButterworthFilterAlgorithm(AlgorithmBandType.LowPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}