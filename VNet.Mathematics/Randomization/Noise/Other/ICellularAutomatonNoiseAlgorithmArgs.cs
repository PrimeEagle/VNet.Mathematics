namespace VNet.Mathematics.Randomization.Noise.Other
{
    public interface ICellularAutomatonNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public int Iterations { get; set; }
        public double Threshold { get; set; }
    }
}