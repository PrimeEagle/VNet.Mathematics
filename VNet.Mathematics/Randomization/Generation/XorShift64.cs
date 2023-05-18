namespace VNet.Mathematics.Randomization.Generation;

public class XorShift64 : RandomGenerationBase<ulong, ulong>
{
    private ulong _state;

    public XorShift64()
    {
        _state = Seeds[0];
    }

    public XorShift64(ulong seed) : base(seed)
    {
        _state = Seeds[0];
    }

    public XorShift64(string seed) : base(seed)
    {
        _state = Seeds[0];
    }

    public XorShift64(ulong seed, ulong minValue, ulong maxValue) : base(seed, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public XorShift64(string seed, ulong minValue, ulong maxValue) : base(seed, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public XorShift64(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public override ulong Next()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        _state = x;

        return x % (MaxValue - MinValue + 1) + MinValue;
    }
}