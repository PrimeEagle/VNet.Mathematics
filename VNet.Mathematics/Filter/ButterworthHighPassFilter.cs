using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class ButterworthHighPassFilter : FilterBase
    {
        public ButterworthHighPassFilter(IButterworthHighPassFilterArgs args) : base(args)
        {
            Algorithm = new ButterworthFilterAlgorithm(AlgorithmBandType.HighPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}