namespace VNet.Mathematics.Filter.Arguments
{
    public interface IIirBandStopFilterArgs : IIirFilterArgs, IBandStopFilterArgs
    {
        public double CutoffLowFrequency { get; set; }
        public double CutoffHighFrequency { get; set; }
    }
}