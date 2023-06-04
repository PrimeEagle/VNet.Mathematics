using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class IirBandPassFilter : FilterBase
    {
        public IirBandPassFilter(IIirBandPassFilterArgs args) : base(args)
        {
            Algorithm = new IirFilterAlgorithm(AlgorithmBandType.BandPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}