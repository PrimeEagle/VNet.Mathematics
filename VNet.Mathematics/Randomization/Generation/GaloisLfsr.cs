namespace VNet.Mathematics.Randomization.Generation;

public class GaloisLfsr : RandomGenerationBase<uint, uint>
{
    public GaloisLfsr()
    {
    }

    public GaloisLfsr(IEnumerable<uint> seeds) : base(seeds)
    {
    }

    public GaloisLfsr(IEnumerable<string> seeds) : base(seeds)
    {
    }

    public GaloisLfsr(IEnumerable<uint> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public GaloisLfsr(IEnumerable<string> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public GaloisLfsr(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
    }

    public override uint Next()
    {
        var bit = (Seeds[0] & 1) ^ ((Seeds[0] >> 1) & 1);
        Seeds[0] = (Seeds[0] >> 1) | (bit << 31);

        return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
    }
}