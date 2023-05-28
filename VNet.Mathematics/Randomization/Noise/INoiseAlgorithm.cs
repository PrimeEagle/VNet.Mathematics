namespace VNet.Mathematics.Randomization.Noise
{
    internal interface INoiseAlgorithm : IRandomizationAlgorithm
    {
        public float[] Generate();

        public float[][] ConvertOutput(float[] output);
    }
}
