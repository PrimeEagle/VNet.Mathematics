namespace VNet.Mathematics.Randomization.Generation;

public class AdditiveFeedback : RandomGenerationBase<int, int>
{
    public AdditiveFeedback()
    {
    }

    public AdditiveFeedback(IEnumerable<int> seeds) : base(seeds) { }

    public AdditiveFeedback(IEnumerable<string> seeds) : base(seeds) { }

    public AdditiveFeedback(IEnumerable<int> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) { }

    public AdditiveFeedback(IEnumerable<string> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) { }

    public AdditiveFeedback(int minValue, int maxValue) : base(minValue, maxValue) { }

    public override int Next()
    {
        Seeds[0] = (Seeds[0] + ((Seeds[0] >> 16) + 1)) % (MaxValue - MinValue + 1) + MinValue;
        return Seeds[0];
    }
}