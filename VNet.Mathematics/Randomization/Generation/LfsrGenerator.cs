namespace VNet.Mathematics.Randomization.Generation;

public class LfsrGenerator : RandomGenerationBase
{
    public LfsrGenerator() : base()
    {
    }

    public LfsrGenerator(double seed) : base(seed)
    {
    }

    public override int Next()
    {
        var bit = (((int)Seeds[0] >> 0) ^ ((int)Seeds[0] >> 2) ^ ((int)Seeds[0] >> 3) ^ ((int)Seeds[0] >> 5)) & 1;
        Seeds[0] = ((int)Seeds[0] >> 1) | (bit << 31);

        return (int)((int)Seeds[0] & 0xFFFFFFFF);
    }
}