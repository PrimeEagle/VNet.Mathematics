namespace VNet.Mathematics.Randomization.Generation;

public class MersenneTwisterMT19937Generator : RandomGenerationBase<ulong, ulong>
{
    private const int N = 624;
    private const int M = 397;
    private const ulong MatrixA = 0x9908B0DFUL;
    private const ulong UpperMask = 0x80000000UL;
    private const ulong LowerMask = 0x7FFFFFFFUL;

    private readonly ulong[] _state;
    private int _index;


    public MersenneTwisterMT19937Generator()
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public MersenneTwisterMT19937Generator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public MersenneTwisterMT19937Generator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public MersenneTwisterMT19937Generator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public MersenneTwisterMT19937Generator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public MersenneTwisterMT19937Generator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = new ulong[N];
        _index = N + 1;
        Initialize(Seeds[0]);
    }

    public override ulong Next()
    {
        if (_index >= N)
            GenerateNumbers();

        var y = _state[_index++];
        y ^= y >> 11;
        y ^= (y << 7) & 0x9D2C5680UL;
        y ^= (y << 15) & 0xEFC60000UL;
        y ^= y >> 18;

        return y % (MaxValue - MinValue + 1) + MinValue;
    }

    private void Initialize(ulong seed)
    {
        _state[0] = seed & 0xFFFFFFFFUL;
        for (var i = 1; i < N; i++) _state[i] = (1812433253UL * (_state[i - 1] ^ (_state[i - 1] >> 30)) + (ulong) i) & 0xFFFFFFFFUL;
    }

    private void GenerateNumbers()
    {
        for (var i = 0; i < N - M; i++)
        {
            var x = (_state[i] & UpperMask) | (_state[i + 1] & LowerMask);
            _state[i] = _state[i + M] ^ (x >> 1) ^ ((x & 1) != 0 ? MatrixA : 0UL);
        }

        for (var i = N - M; i < N - 1; i++)
        {
            var x = (_state[i] & UpperMask) | (_state[i + 1] & LowerMask);
            _state[i] = _state[i + (M - N)] ^ (x >> 1) ^ ((x & 1) != 0 ? MatrixA : 0UL);
        }

        var x0 = (_state[N - 1] & UpperMask) | (_state[0] & LowerMask);
        _state[N - 1] = _state[M - 1] ^ (x0 >> 1) ^ ((x0 & 1) != 0 ? MatrixA : 0UL);

        _index = 0;
    }
}