namespace VNet.Mathematics.Filter;

public interface IFilterArgs
{
    public int Order { get; set; }
    public double SampleRate { get; set; }
    public double CutoffFrequency { get; set; }
    public double Bandwidth { get; set; }
    public BandType BandType { get; set; }
    public FilterType FilterType { get; set; }
}