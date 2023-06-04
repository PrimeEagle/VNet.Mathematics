using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class ButterworthBandStopFilter : FilterBase
    {
        public ButterworthBandStopFilter(IButterworthBandStopFilterArgs args) : base(args)
        {
            Algorithm = new ButterworthFilterAlgorithm(AlgorithmBandType.BandStop, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}