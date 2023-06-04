namespace VNet.Mathematics.Filter;

public interface IFilter
{
    public double[] Filter(double[] input);
    public bool IsValid();
}