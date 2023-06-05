namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface IGrayNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double BlueNoiseWeight { get; set; }
        public double WhiteNoiseWeight { get; set; }
    }
}