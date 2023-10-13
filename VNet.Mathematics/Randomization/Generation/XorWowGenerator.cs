namespace VNet.Mathematics.Randomization.Generation;

public class XorWowGenerator : RandomGenerationBase
{
    private readonly List<uint> _state;


    public XorWowGenerator()
    {
        _state = new List<uint>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(5);
    }

    public XorWowGenerator(double seed1, double seed2, double seed3, double seed4, double seed5)
    {
        _state = new List<uint>();
        this.Seeds = new List<double>()
        {
            seed1,
            seed2,
            seed3,
            seed4,
            seed5
        };
    }

    public override int Next()
    {
        var t = _state[3];
        var s = _state[0];

        _state[3] = _state[2];
        _state[2] = _state[1];
        _state[1] = s;
        t ^= t >> 2;
        t ^= t << 1;
        t ^= s ^ (s << 4);
        _state[0] = t;

        return (int)(t & 0xFFFFFFFF);
    }
}