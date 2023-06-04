using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter;

public class BandPassFilter : FilterBase, ILowPassFilter
{
    public BandPassFilter(IBandPassFilterArgs args) : base(args)
    {
    }

    public override bool IsValid()
    {
        return base.IsValid() && Args.BandType == BandType.BandPass;
    }
}