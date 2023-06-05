namespace VNet.Mathematics.Randomization.Generation;

public class XorShift32Generator : RandomGenerationBase<uint, uint>
{
    private uint _state;

    public XorShift32Generator()
    {
        _state = Seeds[0];
    }

    public XorShift32Generator(uint seed) : base(seed)
    {
        _state = Seeds[0];
    }

    public XorShift32Generator(string seed) : base(seed)
    {
        _state = Seeds[0];
    }

    public XorShift32Generator(uint seed, uint minValue, uint maxValue) : base(seed, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public XorShift32Generator(string seed, uint minValue, uint maxValue) : base(seed, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public XorShift32Generator(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public override uint Next()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        _state = x;

        return x % (MaxValue - MinValue + 1) + MinValue;
    }
}