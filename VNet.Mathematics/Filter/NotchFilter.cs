using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter;

public class NotchFilter : FilterBase, ILowPassFilter
{
    public NotchFilter(INotchFilterArgs args) : base(args)
    {
    }

    public override bool IsValid()
    {
        return base.IsValid() && Args.BandType == BandType.Notch;
    }
}