﻿namespace VNet.Mathematics.Filter.Arguments
{
    public interface IIirHighPassFilterArgs : IIirFilterArgs, IHighPassFilterArgs
    {
        public double CutoffFrequency { get; set; }
        public double Bandwidth { get; set; }
    }
}