namespace VNet.Mathematics.Filter;

public class LowPassFilter : ILowPassFilter
{
    private readonly IFilterAlgorithm _filterAlgorithm;

    public LowPassFilter(IFilterAlgorithm filterAlgorithm)
    {
        _filterAlgorithm = filterAlgorithm;
    }

    public double[] Filter(double[] input, IFilterArgs args)
    {
        if (!IsValid(args)) throw new ArgumentException("Parameters are not configured correctly.");
        return _filterAlgorithm.Apply(input, args);
    }

    public bool IsValid(IFilterArgs args)
    {
        var valid = _filterAlgorithm.IsValid(args);

        return valid;
    }
}