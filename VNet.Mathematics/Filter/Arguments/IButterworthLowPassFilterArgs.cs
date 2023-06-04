namespace VNet.Mathematics.Filter.Arguments
{
    public interface IButterworthLowPassFilterArgs : IButterworthFilterArgs, ILowPassFilterArgs
    {
        public double PassBandFrequency { get; set; }
        public double StopBandFrequency { get; set; }
    }
}