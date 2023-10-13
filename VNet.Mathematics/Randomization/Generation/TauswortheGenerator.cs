namespace VNet.Mathematics.Randomization.Generation;

public class TauswortheGenerator : RandomGenerationBase
{
    private ulong _state;


    public TauswortheGenerator() : base()
    {
        _state = (ulong)Seeds[0];
    }

    public TauswortheGenerator(double seed) : base(seed)
    {
        _state = (ulong)Seeds[0];
    }

    public override int Next()
    {
        var b = ((_state << 13) ^ _state) >> 19;
        var c = ((_state << 17) ^ _state) >> 22;
        var d = ((_state << 5) ^ _state) >> 24;

        _state = (b ^ c ^ d) & 0xFFFFFFFF;

        return (int)_state;
    }
}