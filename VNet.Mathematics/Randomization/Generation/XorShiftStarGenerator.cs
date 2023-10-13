namespace VNet.Mathematics.Randomization.Generation;

public class XorShiftStarGenerator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public XorShiftStarGenerator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public XorShiftStarGenerator(double seed1, double seed2)
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
        _state[1] = s1 ^ s0 ^ (s1 >> 18) ^ (s0 >> 5);

        return (int)(_state[1] & 0xFFFFFFFF);
    }
}