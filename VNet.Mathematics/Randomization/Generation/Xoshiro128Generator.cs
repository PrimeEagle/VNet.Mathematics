namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro128Generator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public Xoshiro128Generator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public Xoshiro128Generator(double seed1, double seed2)
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

        return (int)result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}