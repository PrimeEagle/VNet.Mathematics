namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro64Generator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public Xoshiro64Generator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public Xoshiro64Generator(double seed1, double seed2)
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
        var result = RotateLeft(_state[0] + _state[1], 17) + _state[0];

        _state[1] ^= _state[0];
        _state[0] = RotateLeft(_state[0], 49) ^ _state[1] ^ (_state[1] << 21);
        _state[1] = RotateLeft(_state[1], 28);

        return (int)(result & 0xFFFFFFFF);
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}