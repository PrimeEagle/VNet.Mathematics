namespace VNet.Mathematics.Randomization.Noise
{
    public class WhiteNoise : NoiseBase
    {
        public WhiteNoise(int seed, int[] dimensionSizes, Random random) : base(seed, dimensionSizes, random)
        {
        }

        public WhiteNoise(int[] dimensionSizes) : base(dimensionSizes)
        {
        }

        public WhiteNoise(int seed, int[] dimensions) : base(seed, dimensions)
        {
        }

        public override float[] Generate()
        {
            for (int i = 0; i < TotalSize(); i++)
            {
                noise[i] = (float)random.NextDouble() * 2 - 1;
            }

            return noise;
        }
    }
}