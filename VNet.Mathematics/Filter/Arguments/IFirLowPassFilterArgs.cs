﻿namespace VNet.Mathematics.Filter.Arguments
{
    public interface IFirLowPassFilterArgs : IFirFilterArgs, ILowPassFilterArgs
    {
        public double CutoffFrequency { get; set; }
        public double DcGain { get; set; }
    }
}