#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace VNet.Mathematics.Randomization.Generation;

public class Xoroshiro128Generator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public Xoroshiro128Generator()
    {
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public Xoroshiro128Generator(double seed1, double seed2)
    {
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
        var result = s0 + s1;

        s1 ^= s0;
        _state[0] = RotateLeft(s0, 55) ^ s1 ^ (s1 << 14);
        _state[1] = RotateLeft(s1, 36);

        return (int)result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}