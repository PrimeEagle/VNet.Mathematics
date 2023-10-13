namespace VNet.Mathematics.Randomization.Generation;

public class PcgGenerator : RandomGenerationBase
{
    private const ulong Increment = 1442695040888963407;
    private ulong _state;


    public PcgGenerator() : base()
    {
        _state = (ulong)Seeds[0];
    }

    public PcgGenerator(double seed) : base(seed)
    {
        _state = (ulong)Seeds[0];
    }

   public override int Next()
    {
        var oldState = _state;
        _state = oldState * 6364136223846793005UL + Increment;
        var xorShifted = ((oldState >> 18) ^ oldState) >> 27;
        var rotate = oldState >> 59;
        var randomValue = (xorShifted >> (int) rotate) | (xorShifted << (int) (rotate ^ 31));

        return (int)((long)randomValue & 0xFFFFFFFF);
    }
}