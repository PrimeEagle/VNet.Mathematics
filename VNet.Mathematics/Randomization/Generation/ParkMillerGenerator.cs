namespace VNet.Mathematics.Randomization.Generation;

public class ParkMillerGenerator : RandomGenerationBase
{
    public ParkMillerGenerator()
    {
    }

    public ParkMillerGenerator(double seed) : base(seed)
    {
    }

   public override int Next()
    {
        Seeds[0] = Seeds[0] * 16807UL % 2147483647UL;

        return (int)((long)Seeds[0] & 0xFFFFFFFF);
    }
}