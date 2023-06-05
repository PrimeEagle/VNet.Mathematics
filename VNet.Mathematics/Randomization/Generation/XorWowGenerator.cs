namespace VNet.Mathematics.Randomization.Generation;

public class XorWowGenerator : RandomGenerationBase<uint, uint>
{
    private readonly List<uint> _state;
    protected uint NumberOfSeeds = 5;


    public XorWowGenerator()
    {
        _state = Seeds;
    }

    public XorWowGenerator(IEnumerable<uint> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorWowGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public XorWowGenerator(IEnumerable<uint> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorWowGenerator(IEnumerable<string> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public XorWowGenerator(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override uint Next()
    {
        var t = _state[3];
        var s = _state[0];

        _state[3] = _state[2];
        _state[2] = _state[1];
        _state[1] = s;
        t ^= t >> 2;
        t ^= t << 1;
        t ^= s ^ (s << 4);
        _state[0] = t;

        return t % (MaxValue - MinValue + 1) + MinValue;
    }
}