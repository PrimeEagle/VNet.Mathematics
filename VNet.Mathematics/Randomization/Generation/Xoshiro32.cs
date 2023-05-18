namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro32 : RandomGenerationBase<uint, uint>
{
    private readonly List<uint> _state;
    protected new uint NumberOfSeeds = 4;


    public Xoshiro32()
    {
        _state = Seeds;
    }

    public Xoshiro32(IEnumerable<uint> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro32(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro32(IEnumerable<uint> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro32(IEnumerable<string> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro32(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override uint Next()
    {
        var result = _state[0] + _state[3];

        var t = _state[1] << 9;

        _state[2] ^= _state[0];
        _state[3] ^= _state[1];
        _state[1] ^= _state[2];
        _state[0] ^= _state[3];

        _state[2] ^= t;

        _state[3] = (_state[3] << 11) | (_state[3] >> 21);

        return result % (MaxValue - MinValue + 1) + MinValue;
    }
}