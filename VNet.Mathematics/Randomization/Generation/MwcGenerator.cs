namespace VNet.Mathematics.Randomization.Generation;

public class MwcGenerator : RandomGenerationBase
{
    private const ulong DefaultMultiplier = 4294967296UL;
    private const ulong DefaultModulus = 4294967295UL;
    private readonly ulong _modulus;
    private readonly ulong _multiplier;

    private ulong _state;


    public MwcGenerator() : base()
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(double seed) : base(seed)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

   
    public override int Next()
    {
        _state = _multiplier * (_state & _modulus) + (_state >> 32);

        return (int)(_state & 0xFFFFFFFF);
    }
}