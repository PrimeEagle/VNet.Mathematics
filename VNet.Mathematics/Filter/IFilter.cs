namespace VNet.Mathematics.Filter;

public interface IFilter
{
    public double[] Filter(double[] input, IFilterArgs args);
    public bool IsValid(IFilterArgs args);
}