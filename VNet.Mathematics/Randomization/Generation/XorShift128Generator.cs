namespace VNet.Mathematics.Randomization.Generation;

public class XorShift128Generator : RandomGenerationBase
{
    private readonly List<ulong> _state;



    public XorShift128Generator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public XorShift128Generator(double seed1, double seed2)
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
        var s1 = _state[0];
        var s0 = _state[1];
        _state[0] = s0;
        s1 ^= s1 << 23;
        s1 ^= s1 >> 17;
        s1 ^= s0;
        s1 ^= s0 >> 26;
        _state[1] = s1;

        return (int)(s1 & 0xFFFFFFFF);
    }
}