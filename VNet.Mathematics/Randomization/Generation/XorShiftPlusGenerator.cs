namespace VNet.Mathematics.Randomization.Generation;

public class XorShiftPlusGenerator : RandomGenerationBase<ulong, ulong>
{
    protected new uint NumberOfSeeds = 2;
    private readonly List<ulong> _state;
    

    public XorShiftPlusGenerator()
    {
        _state = Seeds;
    }

    public XorShiftPlusGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShiftPlusGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorShiftPlusGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShiftPlusGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorShiftPlusGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var x = _state[0];
        var y = _state[1];
        _state[0] = y;
        x ^= x << 23;
        _state[1] = x ^ y ^ (x >> 18) ^ (y >> 5);

        return _state[1] % (MaxValue - MinValue + 1) + MinValue;
    }
}