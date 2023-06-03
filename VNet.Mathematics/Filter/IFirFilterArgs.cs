namespace VNet.Mathematics.Filter;

public interface IFirFilterArgs : IFilterArgs
{
    public WindowFunction WindowFunction { get; set; }
    public double Sigma { get; set; }
}