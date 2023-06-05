namespace VNet.Mathematics.Randomization.Generation;

public class ParkMillerGenerator : RandomGenerationBase<ulong, ulong>
{
    public ParkMillerGenerator()
    {
    }

    public ParkMillerGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
    }

    public ParkMillerGenerator(IEnumerable<string> seeds) : base(seeds)
    {
    }

    public ParkMillerGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public ParkMillerGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
    }

    public ParkMillerGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
    }

    public override ulong Next()
    {
        Seeds[0] = Seeds[0] * 16807UL % 2147483647UL;

        return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
    }
}