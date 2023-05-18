namespace VNet.Mathematics.Randomization.Generation;

public class Xoroshiro128StarStar : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 2;


    public Xoroshiro128StarStar()
    {
        _state = Seeds;
    }

    public Xoroshiro128StarStar(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoroshiro128StarStar(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoroshiro128StarStar(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoroshiro128StarStar(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoroshiro128StarStar(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var s0 = _state[0];
        var s1 = _state[1];
        var result = RotateLeft(s0 * 5, 7) * 9;

        _state[1] ^= s0;
        _state[0] = RotateLeft(s0, 24) ^ _state[1] ^ (s1 << 16);
        _state[1] = RotateLeft(s1, 37);

        return result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}