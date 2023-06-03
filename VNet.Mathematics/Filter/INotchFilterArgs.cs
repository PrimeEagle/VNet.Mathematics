namespace VNet.Mathematics.Filter;

public interface INotchFilterArgs : IFilterArgs
{
    public double LowCutoffFrequency { get; set; }
    public double HighCutoffFrequency { get; set; }
}