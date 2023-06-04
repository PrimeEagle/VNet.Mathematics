using VNet.Mathematics.Filter.Algorithms;

namespace VNet.Mathematics.Filter.Arguments
{
    public class IirLowPassFilterArgs : IIirLowPassFilterArgs
    {
        public double CutoffFrequency { get; set; }
        public double Bandwidth { get; set; }
        public double SamplingRate { get; set; }
        public AlgorithmBandType BandType { get; set; }
    }
}