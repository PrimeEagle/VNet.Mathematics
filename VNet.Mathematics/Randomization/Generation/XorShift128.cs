namespace VNet.Mathematics.Randomization.Generation;

public class XorShift128 : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 2;


    public XorShift128()
    {
        _state = Seeds;
    }

    public XorShift128(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShift128(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShift128(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShift128(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShift128(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var s1 = _state[0];
        var s0 = _state[1];
        _state[0] = s0;
        s1 ^= s1 << 23;
        s1 ^= s1 >> 17;
        s1 ^= s0;
        s1 ^= s0 >> 26;
        _state[1] = s1;

        return s1 % (MaxValue - MinValue + 1) + MinValue;
    }
}