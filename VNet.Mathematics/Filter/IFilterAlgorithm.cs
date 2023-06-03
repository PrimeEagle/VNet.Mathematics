namespace VNet.Mathematics.Filter;

public interface IFilterAlgorithm
{
    public double[] Apply(double[] input, IFilterArgs args);
    public bool IsValid(IFilterArgs args);
}