namespace VNet.Mathematics.Randomization.Generation;

public class XorShift32Generator : RandomGenerationBase
{
    private uint _state;

    public XorShift32Generator() : base()
    {
    }

    public XorShift32Generator(double seed) : base(seed)
    {
    }

    public override int Next()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 17;
        x ^= x << 5;
        _state = x;

        return (int)(x & 0xFFFFFFFF);
    }
}