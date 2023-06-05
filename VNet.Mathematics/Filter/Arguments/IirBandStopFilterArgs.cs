﻿using VNet.Mathematics.Filter.Algorithms;

namespace VNet.Mathematics.Filter.Arguments
{
    public class IirBandStopFilterArgs : IIirBandStopFilterArgs
    {
        public double CutoffLowFrequency { get; set; }
        public double CutoffHighFrequency { get; set; }
        public double SamplingRate { get; set; }
        public AlgorithmBandType BandType { get; set; }
    }
}