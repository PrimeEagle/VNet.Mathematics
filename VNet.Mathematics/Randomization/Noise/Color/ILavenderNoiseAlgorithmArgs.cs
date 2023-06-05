namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface ILavenderNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double WhiteNoiseWeight { get; set; }
        public double PinkNoiseWeight { get; set; }
    }
}