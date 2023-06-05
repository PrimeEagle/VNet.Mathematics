namespace VNet.Mathematics.Randomization.Generation;

public class AdditiveFeedbackGenerator : RandomGenerationBase<int, int>
{
    public AdditiveFeedbackGenerator()
    {
    }

    public AdditiveFeedbackGenerator(IEnumerable<int> seeds) : base(seeds) { }

    public AdditiveFeedbackGenerator(IEnumerable<string> seeds) : base(seeds) { }

    public AdditiveFeedbackGenerator(IEnumerable<int> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) { }

    public AdditiveFeedbackGenerator(IEnumerable<string> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) { }

    public AdditiveFeedbackGenerator(int minValue, int maxValue) : base(minValue, maxValue) { }

    public override int Next()
    {
        Seeds[0] = (Seeds[0] + ((Seeds[0] >> 16) + 1)) % (MaxValue - MinValue + 1) + MinValue;
        return Seeds[0];
    }
}