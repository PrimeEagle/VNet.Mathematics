using VNet.Mathematics.Filter.Algorithms;

namespace VNet.Mathematics.Filter.Arguments
{
    public class ButterworthLowPassFilterArgs : IButterworthLowPassFilterArgs
    {
        public double PassBandFrequency { get; set; }
        public double StopBandFrequency { get; set; }
        public double PassBandRipple { get; set; }
        public double StopBandAttenuation { get; set; }
        public AlgorithmBandType BandType { get; set; }
    }
}