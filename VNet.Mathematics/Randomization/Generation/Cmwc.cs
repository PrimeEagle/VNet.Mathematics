namespace VNet.Mathematics.Randomization.Generation;

public class Lfg : RandomGenerationBase<int, int>
{
    private const int DefaultLag = 16;
    private int _index;
    private readonly int _lag;

    private readonly int[] _state;
    protected new uint NumberOfSeeds = 2;


    public Lfg()
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public Lfg(IEnumerable<int> seeds) : base(seeds)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public Lfg(IEnumerable<string> seeds) : base(seeds)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public Lfg(IEnumerable<int> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public Lfg(IEnumerable<string> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public Lfg(int minValue, int maxValue) : base(minValue, maxValue)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        Initialize(Seeds[0], Seeds[1]);
    }

    public override int Next()
    {
        var result = (_state[_index] + _state[(_index - _lag + _lag) % _lag]) % (MaxValue - MinValue + 1) + MinValue;
        _state[_index] = result;
        _index = (_index + 1) % _lag;

        return result;
    }

    private void Initialize(int seed1, int seed2)
    {
        _state[0] = seed1;
        _state[1] = seed2;

        for (var i = 2; i < _lag; i++) _state[i] = (_state[i - 1] + _state[i - 2]) % int.MaxValue;
    }
}