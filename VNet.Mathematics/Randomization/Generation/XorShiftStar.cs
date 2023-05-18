namespace VNet.Mathematics.Randomization.Generation;

public class XorShiftStar : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 2;


    public XorShiftStar()
    {
        _state = Seeds;
    }

    public XorShiftStar(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShiftStar(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShiftStar(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShiftStar(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShiftStar(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var s1 = _state[0];
        var s0 = _state[1];
        _state[0] = s0;
        s1 ^= s1 << 23;
        _state[1] = s1 ^ s0 ^ (s1 >> 18) ^ (s0 >> 5);

        return _state[1] % (MaxValue - MinValue + 1) + MinValue;
    }
}