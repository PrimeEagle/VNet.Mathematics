namespace VNet.Mathematics.Randomization.Generation;

public class GaloisLfsrGenerator : RandomGenerationBase
{
    public GaloisLfsrGenerator() : base()
    {
    }

    public GaloisLfsrGenerator(double seed) : base(seed)
    {
    }

    public override int Next()
    {
        var bit = ((int)Seeds[0] & 1) ^ (((int)Seeds[0] >> 1) & 1);
        var result = ((int)Seeds[0] >> 1) | (bit << 31);

        if (result < 0)
        {
            result += int.MaxValue;
        }

        return result;
    }
}