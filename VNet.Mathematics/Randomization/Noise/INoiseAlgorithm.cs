namespace VNet.Mathematics.Randomization.Noise
{
    public interface INoiseAlgorithm : IRandomizationAlgorithm
    {
        public double[,] Generate(INoiseAlgorithmArgs args);
        public double GenerateSingleSample(INoiseAlgorithmArgs args);
    }
}