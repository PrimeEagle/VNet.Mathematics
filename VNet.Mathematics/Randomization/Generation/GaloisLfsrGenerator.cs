namespace VNet.Mathematics.Randomization.Generation;

public class GaloisLfsrGenerator : RandomGenerationBase<uint, uint>
{
    public GaloisLfsrGenerator()
    {
    }

    public GaloisLfsrGenerator(IEnumerable<uint> seeds) : base(seeds)
    {
    }

    public GaloisLfsrGenerator(IEnumerable<string> seeds) : base(seeds)
    {
    }

    public GaloisLfsrGenerator(IEnumerable<uint> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public GaloisLfsrGenerator(IEnumerable<string> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public GaloisLfsrGenerator(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
    }

    public override uint Next()
    {
        var bit = (Seeds[0] & 1) ^ ((Seeds[0] >> 1) & 1);
        Seeds[0] = (Seeds[0] >> 1) | (bit << 31);

        return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
    }
}