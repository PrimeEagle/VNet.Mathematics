namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface ICyanNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double BlueNoiseWeight { get; set; }
        public double GreenNoiseWeight { get; set; }
    }
}