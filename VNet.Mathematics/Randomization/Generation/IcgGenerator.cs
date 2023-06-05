namespace VNet.Mathematics.Randomization.Generation;

public class IcgGenerator : RandomGenerationBase<ulong, ulong>
{
    private const ulong DefaultMultiplier = 6364136223846793005UL;
    private const ulong DefaultIncrement = 1442695040888963407UL;
    private const ulong DefaultModulus = ulong.MaxValue;
    private readonly ulong _increment;
    private readonly ulong _modulus;
    private readonly ulong _multiplier;

    private ulong _state;


    public IcgGenerator()
    {
    }

    public IcgGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public IcgGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public IcgGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public IcgGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public IcgGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _increment = DefaultIncrement;
        _modulus = DefaultModulus;
    }

    public override ulong Next()
    {
        _state = (_state * _multiplier + _increment) % _modulus;

        return _state % (MaxValue - MinValue + 1) + MinValue;
    }
}