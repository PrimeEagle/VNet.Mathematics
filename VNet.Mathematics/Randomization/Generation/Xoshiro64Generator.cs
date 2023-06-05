namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro64Generator : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 2;


    public Xoshiro64Generator()
    {
        _state = Seeds;
    }

    public Xoshiro64Generator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro64Generator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro64Generator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro64Generator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro64Generator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var result = RotateLeft(_state[0] + _state[1], 17) + _state[0];

        _state[1] ^= _state[0];
        _state[0] = RotateLeft(_state[0], 49) ^ _state[1] ^ (_state[1] << 21);
        _state[1] = RotateLeft(_state[1], 28);

        return result % (MaxValue - MinValue + 1) + MinValue;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}