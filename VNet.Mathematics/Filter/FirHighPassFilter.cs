using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class FirHighPassFilter : FilterBase
    {
        public FirHighPassFilter(IFirHighPassFilterArgs args) : base(args)
        {
            Algorithm = new FirFilterAlgorithm(AlgorithmBandType.HighPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}