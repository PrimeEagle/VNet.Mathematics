namespace VNet.Mathematics.Randomization.Generation;

public class CmwcGenerator : RandomGenerationBase<ulong, ulong>
{
    private const int StateSize = 4096;
    private const ulong Multiplier = 18782;
    private const ulong Increment = 362436;
    private const ulong Modulus = 0xFFFFFFFF;

    private readonly ulong[] _state;
    private int _index;

    public CmwcGenerator()
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public override ulong Next()
    {
        var t = Multiplier * _state[_index] + Increment;
        var carry = (uint) (t >> 32);
        _state[_index] = (uint) (t & Modulus);
        _index = (_index + 1) % StateSize;

        var result = _state[_index] ^ carry;

        return result % (MaxValue - MinValue + 1) + MinValue;
    }

    private void Initialize(ulong seed)
    {
        var s = seed;
        for (var i = 0; i < StateSize; i++)
        {
            s = (Multiplier * (s & Modulus) + Increment) & Modulus;
            _state[i] = s;
            s = (s + (ulong) i) & Modulus;
        }
    }
}