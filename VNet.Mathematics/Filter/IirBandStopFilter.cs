using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class IirBandStopFilter : FilterBase
    {
        public IirBandStopFilter(IIirBandStopFilterArgs args) : base(args)
        {
            Algorithm = new IirFilterAlgorithm(AlgorithmBandType.BandStop, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}