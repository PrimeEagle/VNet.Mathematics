namespace VNet.Mathematics.Randomization.Noise
{
    public abstract class NoiseBase : INoiseAlgorithm
    {
        public abstract double[,] Generate(INoiseAlgorithmArgs args);
        public abstract double GenerateSingleSample(INoiseAlgorithmArgs args);
    }
}