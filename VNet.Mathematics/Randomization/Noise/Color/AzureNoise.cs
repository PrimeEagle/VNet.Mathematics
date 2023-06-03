// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Azure noise, also known as sky-blue noise, is a term used to describe noise with a power spectral density that increases at a
// rate higher than blue noise. It emphasizes higher frequencies more than blue noise.
public class AzureNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _blueNoise;
    private INoiseAlgorithm _violetNoise;
    private double _blueNoiseWeight;
    private double _violetNoiseWeight;

    public AzureNoise(double blueNoiseWeight = 0.5, double violetNoiseWeight = 0.5)
    {
        _blueNoise = new BlueNoise();
        _violetNoise = new VioletNoise();
        _blueNoiseWeight = blueNoiseWeight;
        _violetNoiseWeight = violetNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];

        double[,] blueNoiseData = _blueNoise.Generate(args);
        double[,] violetNoiseData = _violetNoise.Generate(args);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double blueNoiseValue = blueNoiseData[i, j];
                double violetNoiseValue = violetNoiseData[i, j];
                result[i, j] = (_blueNoiseWeight * blueNoiseValue + _violetNoiseWeight * violetNoiseValue) * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Azure noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}