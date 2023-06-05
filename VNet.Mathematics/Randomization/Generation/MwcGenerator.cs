namespace VNet.Mathematics.Randomization.Generation;

public class MwcGenerator : RandomGenerationBase<ulong, ulong>
{
    private const ulong DefaultMultiplier = 4294967296UL;
    private const ulong DefaultModulus = 4294967295UL;
    private readonly ulong _modulus;
    private readonly ulong _multiplier;

    private ulong _state;


    public MwcGenerator()
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public MwcGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _multiplier = DefaultMultiplier;
        _modulus = DefaultModulus;
    }

    public override ulong Next()
    {
        _state = _multiplier * (_state & _modulus) + (_state >> 32);

        return _state % (MaxValue - MinValue + 1) + MinValue;
    }
}