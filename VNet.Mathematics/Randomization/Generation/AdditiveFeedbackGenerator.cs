namespace VNet.Mathematics.Randomization.Generation;

public class AdditiveFeedbackGenerator : RandomGenerationBase
{
    public AdditiveFeedbackGenerator() : base()
    {
    }

    public AdditiveFeedbackGenerator(double seed) : base(seed)
    {
    }

    public override int Next()
    {
        lock (Lock)
        {
            var newSeed = (int) Seeds[0] + ((int) Seeds[0] >> 16);

            if (newSeed < 0)
                newSeed = int.MaxValue + newSeed;

            Seeds[0] = newSeed;
         
            return newSeed;
        }
    }
}