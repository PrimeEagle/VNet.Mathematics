using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class FirBandStopFilter : FilterBase
    {
        public FirBandStopFilter(IFirBandStopFilterArgs args) : base(args)
        {
            Algorithm = new FirFilterAlgorithm(AlgorithmBandType.BandStop, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}