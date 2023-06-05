namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface IBlueNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double Radius { get; set; }
        public int MaxAttempts { get; set; }
    }
}