using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class FirBandPassFilter : FilterBase
    {
        public FirBandPassFilter(IFirBandPassFilterArgs args) : base(args)
        {
            Algorithm = new FirFilterAlgorithm(AlgorithmBandType.BandPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}