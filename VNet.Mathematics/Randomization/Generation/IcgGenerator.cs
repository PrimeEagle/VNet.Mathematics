namespace VNet.Mathematics.Randomization.Generation;

public class IcgGenerator : RandomGenerationBase
{
    private const ulong DefaultMultiplier = 6364136223846793005UL;
    private const ulong DefaultIncrement = 1442695040888963407UL;
    private const ulong DefaultModulus = ulong.MaxValue;
    private readonly ulong _increment;
    private readonly ulong _modulus;
    private readonly ulong _multiplier;

    private ulong _state;


    public IcgGenerator() : base()
    {
    }

    public IcgGenerator(double seed) : base(seed)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public override int Next()
    {
        _state = (_state * _multiplier + _increment) % _modulus;

        return (int)_state;
    }
}