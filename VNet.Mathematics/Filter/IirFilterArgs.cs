namespace VNet.Mathematics.Filter;

public interface IIirFilterParameters : IFilterArgs
{
    FilterType FilterType { get; set; }
}