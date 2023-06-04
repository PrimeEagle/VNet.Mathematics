using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class IirLowPassFilter : FilterBase
    {
        public IirLowPassFilter(IIirLowPassFilterArgs args) : base(args)
        {
            Algorithm = new IirFilterAlgorithm(AlgorithmBandType.LowPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}