﻿using VNet.Mathematics.Filter.Algorithms;
using VNet.Mathematics.Filter.Arguments;
// ReSharper disable SuggestBaseTypeForParameterInConstructor

namespace VNet.Mathematics.Filter
{
    internal class FirLowPassFilter : FilterBase
    {
        public FirLowPassFilter(IFirLowPassFilterArgs args) : base(args)
        {
            Algorithm = new FirFilterAlgorithm(AlgorithmBandType.LowPass, args);
        }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}