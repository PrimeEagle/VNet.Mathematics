// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Lavender noise is a term used to describe noise with a power spectral density that increases at a rate of 3 dB per octave. It lies between
// pink noise and white noise in terms of its frequency distribution.
public class LavenderNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _whiteNoise;
    private INoiseAlgorithm _pinkNoise;
    private double _whiteNoiseWeight;
    private double _pinkNoiseWeight;

    public LavenderNoise(double whiteNoiseWeight = 0.5, double pinkNoiseWeight = 0.5)
    {
        _whiteNoise = new WhiteNoise();
        _pinkNoise = new PinkNoise();
        _whiteNoiseWeight = whiteNoiseWeight;
        _pinkNoiseWeight = pinkNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];

        double[,] whiteNoiseData = _whiteNoise.Generate(args);
        double[,] pinkNoiseData = _pinkNoise.Generate(args);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double whiteNoiseValue = whiteNoiseData[i, j];
                double pinkNoiseValue = pinkNoiseData[i, j];
                result[i, j] = (_whiteNoiseWeight * whiteNoiseValue + _pinkNoiseWeight * pinkNoiseValue) * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Lavender noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}