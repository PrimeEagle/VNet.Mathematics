namespace VNet.Mathematics.Randomization.Generation;

public class LfgGenerator : RandomGenerationBase
{
    private const int DefaultLag = 16;
    private int _index;
    private readonly int _lag;

    private readonly int[] _state;
    protected new uint NumberOfSeeds = 2;


    public LfgGenerator()
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;

        this.Seeds = GetSeedsFromTime(2);
        Initialize((int)Seeds[0], (int)Seeds[1]);
    }

    public LfgGenerator(double seed1, double seed2)
    {
        _state = new int[DefaultLag];
        _index = 0;
        _lag = DefaultLag;
        this.Seeds = new List<double>()
        {
            seed1,
            seed2
        };

        Initialize((int)Seeds[0], (int)Seeds[1]);
    }

   public override int Next()
    {
        var result = (_state[_index] + _state[(_index - _lag + _lag) % _lag]) & 0xFFFFFFFF;
        _state[_index] = (int)result;
        _index = (_index + 1) % _lag;

        return (int)result;
    }

    private void Initialize(int seed1, int seed2)
    {
        _state[0] = seed1;
        _state[1] = seed2;

        for (var i = 2; i < _lag; i++) _state[i] = (_state[i - 1] + _state[i - 2]) % int.MaxValue;
    }
}