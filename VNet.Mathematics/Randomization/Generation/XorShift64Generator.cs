namespace VNet.Mathematics.Randomization.Generation;

public class XorShift64Generator : RandomGenerationBase
{
    private ulong _state;

    public XorShift64Generator() : base()
    {
        _state = (ulong)Seeds[0];
    }

    public XorShift64Generator(double seed) : base(seed)
    {
        _state = (ulong)Seeds[0];
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