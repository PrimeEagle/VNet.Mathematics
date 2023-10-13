namespace VNet.Mathematics.Randomization.Generation;

public class Xoroshiro128StarStarGenerator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public Xoroshiro128StarStarGenerator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public Xoroshiro128StarStarGenerator(double seed1, double seed2)
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>()
        {
            seed1,
            seed2
        };
    }

   public override int Next()
    {
        var s0 = _state[0];
        var s1 = _state[1];
        var result = RotateLeft(s0 * 5, 7) * 9;

        _state[1] ^= s0;
        _state[0] = RotateLeft(s0, 24) ^ _state[1] ^ (s1 << 16);
        _state[1] = RotateLeft(s1, 37);

        return (int)result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}