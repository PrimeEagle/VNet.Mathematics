namespace VNet.Mathematics.Randomization.Generation;

public class Well512 : RandomGenerationBase<ulong, uint>
{
    private const int StateSize = 16;
    private const int R = 16;
    private const int M1 = 13;
    private const int M2 = 9;
    private const int M3 = 5;

    private readonly uint[] _state;
    private int _index;


    public Well512()
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public Well512(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public Well512(IEnumerable<string> seeds) : base(seeds)
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public Well512(IEnumerable<ulong> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public Well512(IEnumerable<string> seeds, uint minValue, uint maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public Well512(uint minValue, uint maxValue) : base(minValue, maxValue)
    {
        _state = new uint[StateSize];
        _index = 0;
        Initialize(Seeds[0]);
    }

    public override uint Next()
    {
        var z0 = _state[(_index + 15) & 15];
        var z1 = _state[_index] ^ _state[(_index + M1) & 15] ^ (_state[_index] >> 16) ^ (_state[(_index + M2) & 15] >> 15);
        _state[_index] = z1;
        _state[(_index + 15) & 15] = z0 ^ (z0 << 2) ^ (z1 << 18) ^ (z0 << 28) ^ _state[(_index + M3) & 15] ^ (_state[(_index + 15) & 15] << 13);
        _index = (_index + 15) & 15;

        return z1 % (MaxValue - MinValue + 1) + MinValue;
    }

    private void Initialize(ulong seed)
    {
        var s = seed;
        for (var i = 0; i < StateSize; i++)
        {
            s ^= s >> 19;
            s ^= s << 11;
            s ^= s >> 8;
            _state[i] = (uint) (s & uint.MaxValue);
        }
    }

    private void Initialize(ulong[] seed)
    {
        for (var i = 0; i < StateSize && i < seed.Length; i++) _state[i] = (uint) (seed[i] & uint.MaxValue);
    }
}