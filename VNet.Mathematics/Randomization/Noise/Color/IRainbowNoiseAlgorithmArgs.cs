namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface IRainbowNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double SamplingRate { get; set; }
        public int Octaves { get; set; }
    }
}