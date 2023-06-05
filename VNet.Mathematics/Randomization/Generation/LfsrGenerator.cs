namespace VNet.Mathematics.Randomization.Generation;

public class LfsrGenerator : RandomGenerationBase<ulong, ulong>
{
    public LfsrGenerator()
    {
    }

    public LfsrGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
    }

    public LfsrGenerator(IEnumerable<string> seeds) : base(seeds)
    {
    }

    public LfsrGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public LfsrGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public LfsrGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
    }

    public override ulong Next()
    {
        var bit = ((Seeds[0] >> 0) ^ (Seeds[0] >> 2) ^ (Seeds[0] >> 3) ^ (Seeds[0] >> 5)) & 1;
        Seeds[0] = (Seeds[0] >> 1) | (bit << 31);

        return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
    }
}