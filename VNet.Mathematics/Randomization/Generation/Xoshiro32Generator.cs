namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro32Generator : RandomGenerationBase
{
    private readonly List<uint> _state;


    public Xoshiro32Generator()
    {
        _state = new List<uint>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(4);
    }

    public Xoshiro32Generator(double seed1, double seed2, double seed3, double seed4)
    {
        _state = new List<uint>();
        this.Seeds = new List<double>()
        {
            seed1,
            seed2,
            seed3,
            seed4
        };
    }

    public override int Next()
    {
        var result = _state[0] + _state[3];

        var t = _state[1] << 9;

        _state[2] ^= _state[0];
        _state[3] ^= _state[1];
        _state[1] ^= _state[2];
        _state[0] ^= _state[3];

        _state[2] ^= t;

        _state[3] = (_state[3] << 11) | (_state[3] >> 21);

        return (int)(result & 0xFFFFFFFF);
    }
}