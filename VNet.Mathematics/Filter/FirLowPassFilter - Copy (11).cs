using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class FirLowPassFilter : FilterBase
    {
        public FirLowPassFilter(IFirLowPassFilterArgs args) : base(args)
        {
            args.BandType = AlgorithmBandType.LowPass;
            Algorithm = new FirFilterAlgorithm(args);
        }

        public override bool IsValid()
        {
            var valid = base.IsValid();

            if (valid) { }

            return valid;
        }
    }
}