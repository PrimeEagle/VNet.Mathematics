using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class MedianDenoiseFilter : FilterBase
    {
        public MedianDenoiseFilter(IMedianFilterArgs args) : base(args)
        {
            Algorithm = new MedianFilterAlgorithm(AlgorithmBandType.Denoise, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}