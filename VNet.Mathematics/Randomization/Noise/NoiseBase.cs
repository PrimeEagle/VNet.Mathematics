using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("VNet.Noise.Tests")]

namespace VNet.Mathematics.Randomization.Noise
{
    public class NoiseBase : INoiseAlgorithm
    {
        protected int seed;
        protected int[] dimensionSizes;
        protected Random random;
        protected float[] noise;
        protected float[][] transformedNoise;

        public NoiseBase(int seed, int[] dimensionSizes, Random random)
        {
            this.seed = seed;
            this.dimensionSizes = dimensionSizes;
            this.random = random;
            InitializeOutput();
        }

        public NoiseBase(int[] dimensionSizes)
        {
            this.dimensionSizes = dimensionSizes;
            seed = Guid.NewGuid().GetHashCode();
            random = new Random(seed);
            InitializeOutput();
        }

        public NoiseBase(int seed, int[] dimensions)
        {
            dimensionSizes = dimensions;
            this.seed = seed;
            random = new Random(this.seed);
            InitializeOutput();
        }

        public NoiseBase()
        {
            throw new InvalidOperationException();
        }

        protected int TotalSize()
        {
            int size = 1;
            foreach (int ds in dimensionSizes)
            {
                if (ds <= 0)
                {
                    throw new ArgumentException("The length of each dimension must be greater than zero.");
                }

                size *= ds;
            }

            return size;
        }

        [MemberNotNull(nameof(noise))]
        [MemberNotNull(nameof(transformedNoise))]
        private void InitializeOutput()
        {
            noise = new float[TotalSize()];
            transformedNoise = new float[noise.Length][];

            // initialize output array
            for (int i = 0; i < dimensionSizes.Length; i++)
            {
                int size = dimensionSizes[i];
                transformedNoise[i] = new float[size];
            }
        }

        public virtual float[] Generate()
        {
            throw new NotImplementedException();
        }

        public virtual float[][] ConvertOutput(float[] output)
        {
            int currentIndex = 0;
            for (int i = 0; i < dimensionSizes.Length; i++)
            {
                transformedNoise[i] = new float[dimensionSizes[i]];
                Array.Copy(noise, currentIndex, transformedNoise[i], 0, dimensionSizes[i]);
                currentIndex += dimensionSizes[i];
            }

            return transformedNoise;
        }
    }
}