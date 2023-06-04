using VNet.Mathematics.Filter.Algorithms;

namespace VNet.Mathematics.Filter.Arguments
{
    public class FirHighPassFilterArgs : IFirHighPassFilterArgs
    {
        public double CutoffFrequency { get; set; }
        public int Order { get; set; }
        public double SamplingRate { get; set; }
        public WindowFunction WindowFunction { get; set; }
        public AlgorithmBandType BandType { get; set; }
        public double Sigma { get; set; }
    }
}