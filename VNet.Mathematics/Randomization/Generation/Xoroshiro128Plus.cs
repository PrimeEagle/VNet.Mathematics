namespace VNet.Mathematics.Randomization.Generation;

public class Xoroshiro128Plus : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 2;


    public Xoroshiro128Plus()
    {
        _state = Seeds;
    }

    public Xoroshiro128Plus(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoroshiro128Plus(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoroshiro128Plus(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoroshiro128Plus(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoroshiro128Plus(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var s0 = _state[0];
        var s1 = _state[1];
        var result = s0 + s1;

        _state[1] ^= s0;
        _state[0] = RotateLeft(s0, 55) ^ _state[1] ^ (_state[1] << 14);
        _state[1] = RotateLeft(_state[1], 36);

        return result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}