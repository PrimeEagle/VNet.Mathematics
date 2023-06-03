// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Gold noise, sometimes referred to as golden noise, is a term used to describe noise with a power spectral density that increases at a
// rate higher than pink noise. It exhibits a higher emphasis on higher frequencies compared to pink noise.
public class GoldNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _orangeNoise;
    private INoiseAlgorithm _brownNoise;
    private double _orangeNoiseWeight;
    private double _brownNoiseWeight;

    public GoldNoise(double orangeNoiseWeight = 0.5, double brownNoiseWeight = 0.5)
    {
        _orangeNoise = new OrangeNoise();
        _brownNoise = new BrownNoise();
        _orangeNoiseWeight = orangeNoiseWeight;
        _brownNoiseWeight = brownNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];

        double[,] orangeNoiseData = _orangeNoise.Generate(args);
        double[,] brownNoiseData = _brownNoise.Generate(args);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double orangeNoiseValue = orangeNoiseData[i, j];
                double brownNoiseValue = brownNoiseData[i, j];
                result[i, j] = (_orangeNoiseWeight * orangeNoiseValue + _brownNoiseWeight * brownNoiseValue) * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Gold noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}