namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface IPinkNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double SamplingRate { get; set; }
        public int Octaves { get; set; }
    }
}