namespace VNet.Mathematics.Randomization.Generation;

public class CmwcGenerator : RandomGenerationBase
{
    private const int StateSize = 4096;
    private const ulong Multiplier = 18782;
    private const ulong Increment = 362436;
    private const ulong Modulus = 0xFFFFFFFF;
    private readonly ulong[] _state;
    private int _index;

    public CmwcGenerator() : base()
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public CmwcGenerator(double seed) : base(seed)
    {
        _state = new ulong[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }
    
    public override int Next()
    {
        lock (Lock)
        {
            var t = Multiplier * _state[_index] + Increment;
            var carry = (uint) (t >> 32);
            _state[_index] = (uint) (t & Modulus);
            _index = (_index + 1) % StateSize;

            var result = _state[_index] ^ carry;

            return (int)result;
        }
    }

    private void Initialize(double seed)
    {
        var s = seed;
        for (var i = 0; i < StateSize; i++)
        {
            s = (Multiplier * ((ulong)s & Modulus) + Increment) & Modulus;
            _state[i] = (ulong)s;
            s = ((ulong)s + (ulong) i) & Modulus;
        }
    }
}