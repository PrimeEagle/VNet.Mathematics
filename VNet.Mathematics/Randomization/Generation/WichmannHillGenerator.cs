namespace VNet.Mathematics.Randomization.Generation;

public class WichmannHillGenerator : RandomGenerationBase
{
    private const ulong Modulus1 = 30269;
    private const ulong Modulus2 = 30307;
    private const ulong Modulus3 = 30323;

    private ulong _x;
    private ulong _y;
    private ulong _z;


    public WichmannHillGenerator() : base()
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public WichmannHillGenerator(double seed) : base(seed)
    {
        _x = 1;
        _y = 1;
        _z = 1;
    }

    public override int Next()
    {
        _x = _x * 171 % Modulus1;
        _y = _y * 172 % Modulus2;
        _z = _z * 170 % Modulus3;

        return (int)((_x + _y + _z) & 0xFFFFFFFF);
    }
}