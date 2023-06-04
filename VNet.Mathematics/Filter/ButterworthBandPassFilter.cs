using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class ButterworthBandPassFilter : FilterBase
    {
        public ButterworthBandPassFilter(IButterworthBandPassFilterArgs args) : base(args)
        {
            Algorithm = new ButterworthFilterAlgorithm(AlgorithmBandType.BandPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}