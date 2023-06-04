namespace VNet.Mathematics.Randomization.Noise
{
    public interface INoiseAlgorithm : IRandomizationAlgorithm
    {
        public double[,] GenerateRaw();
        public double[,] Generate();
        public double GenerateSingleSampleRaw();
        public double GenerateSingleSample();
    }
}