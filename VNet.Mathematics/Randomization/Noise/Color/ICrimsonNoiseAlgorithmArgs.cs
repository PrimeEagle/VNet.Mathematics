namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface ICrimsonNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double RedNoiseWeight { get; set; }
        public double VioletNoiseWeight { get; set; }
    }
}