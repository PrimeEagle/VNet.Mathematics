namespace VNet.Mathematics.Filter;

public interface IBandPassFilterArgs : IFilterArgs
{
    public double LowCutoffFrequency { get; set; }
    public double HighCutoffFrequency { get; set; }
}