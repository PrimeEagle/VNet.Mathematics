using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter;

public class LowPassFilter : FilterBase, ILowPassFilter
{
    public LowPassFilter(IFilterArgs args) : base(args)
    {
    }

    public override bool IsValid()
    {
        return base.IsValid() && Args.BandType == BandType.LowPass;
    }
}