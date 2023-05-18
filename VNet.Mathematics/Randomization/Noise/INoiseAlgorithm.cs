namespace VNet.Mathematics.Randomization.Noise
{
    internal interface INoiseAlgorithm
    {
        public float[] Generate();

        public float[][] ConvertOutput(float[] output);
    }
}
