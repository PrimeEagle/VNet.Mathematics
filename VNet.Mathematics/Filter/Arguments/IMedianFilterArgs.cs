namespace VNet.Mathematics.Filter.Arguments;

public interface IMedianFilterArgs : IFilterArgs
{
    public int Order { get; set; }
}