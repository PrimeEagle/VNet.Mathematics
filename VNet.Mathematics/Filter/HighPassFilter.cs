using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter;

public class HighPassFilter : FilterBase, ILowPassFilter
{
    public HighPassFilter(IHighPassFilterArgs args) : base(args)
    {
    }

    public override bool IsValid()
    {
        return base.IsValid() && Args.BandType == BandType.HighPass;
    }
}