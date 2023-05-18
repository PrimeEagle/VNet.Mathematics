namespace VNet.Mathematics.Randomization.Generation;

public class WichmannHill : RandomGenerationBase<ulong, ulong>
{
    private const ulong Modulus1 = 30269;
    private const ulong Modulus2 = 30307;
    private const ulong Modulus3 = 30323;
    private const ulong SeedMax = Modulus1 * Modulus2 * Modulus3;
    private ulong _seed;

    private ulong _x;
    private ulong _y;
    private ulong _z;


    public WichmannHill()
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHill(IEnumerable<ulong> seeds) : base(seeds)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHill(IEnumerable<string> seeds) : base(seeds)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHill(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHill(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHill(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public override ulong Next()
    {
        _x = _x * 171 % Modulus1;
        _y = _y * 172 % Modulus2;
        _z = _z * 170 % Modulus3;

        return (_x + _y + _z) % (MaxValue - MinValue + 1) + MinValue;
    }
}