using VNet.Mathematics.Filter.Algorithms;

namespace VNet.Mathematics.Filter.Arguments;

public interface IFilterArgs
{
    public AlgorithmBandType BandType { get; set; }
}