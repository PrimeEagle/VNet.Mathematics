namespace VNet.Mathematics.Filter;

public class FilterParameters : IFirFilterArgs, IIirFilterParameters, ILowPassFilterArgs, IHighPassFilterArgs, IBandPassFilterArgs, INotchFilterArgs
{
    public double SampleRate { get; set; }
    public double CutoffFrequency { get; set; }
    public double Bandwidth { get; set; }
    public BandType BandType { get; set; }
    public int Order { get; set; }
    public WindowFunction WindowFunction { get; set; }
    public double Sigma { get; set; }
    public FilterType FilterType { get; set; }
    public double LowCutoffFrequency { get; set; }
    public double HighCutoffFrequency { get; set; }
}