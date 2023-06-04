using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter;

public class BandStopFilter : FilterBase, ILowPassFilter
{
    public BandStopFilter(ILowPassFilterArgs args) : base(args)
    {
    }

    public override bool IsValid()
    {
        return base.IsValid() && Args.BandType == BandType.BandStop;
    }
}