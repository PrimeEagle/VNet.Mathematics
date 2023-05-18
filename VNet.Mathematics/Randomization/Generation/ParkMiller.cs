namespace VNet.Mathematics.Randomization.Generation;

public class ParkMiller : RandomGenerationBase<ulong, ulong>
{
    public ParkMiller()
    {
    }

    public ParkMiller(IEnumerable<ulong> seeds) : base(seeds)
    {
    }

    public ParkMiller(IEnumerable<string> seeds) : base(seeds)
    {
    }

    public ParkMiller(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public ParkMiller(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public ParkMiller(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
    }

    public override ulong Next()
    {
        Seeds[0] = Seeds[0] * 16807UL % 2147483647UL;

        return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
    }
}