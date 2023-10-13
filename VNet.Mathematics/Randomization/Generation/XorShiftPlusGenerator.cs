namespace VNet.Mathematics.Randomization.Generation;

public class XorShiftPlusGenerator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public XorShiftPlusGenerator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(2);
    }

    public XorShiftPlusGenerator(double seed1, double seed2)
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
        var x = _state[0];
        var y = _state[1];
        _state[0] = y;
        x ^= x << 23;
        _state[1] = x ^ y ^ (x >> 18) ^ (y >> 5);

        return (int)(_state[1] & 0xFFFFFFFF);
    }
}