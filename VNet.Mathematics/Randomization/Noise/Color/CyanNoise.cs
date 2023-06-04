// ReSharper disable UnusedMember.Global

using System.Numerics;
using VNet.Mathematics.Filter;

namespace VNet.Mathematics.Randomization.Noise.Color;
// Cyan noise is a term used to describe noise with a power spectral density that increases at a rate between blue noise and white noise.
// It lies between the two in terms of its frequency distribution
public class CyanNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _blueNoise;
    private INoiseAlgorithm _greenNoise;
    private double _blueNoiseWeight;
    private double _greenNoiseWeight;

    public CyanNoise(double blueNoiseWeight = 0.5, double greenNoiseWeight = 0.5)
    {
        _blueNoise = new BlueNoise();
        _greenNoise = new GreenNoise();
        _blueNoiseWeight = blueNoiseWeight;
        _greenNoiseWeight = greenNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int args.Width = args.Width;
        int args.Height = args.Height;

        double[,] result = new double[args.Height, args.Width];

        double[,] blueNoiseData = _blueNoise.Generate(args);
        double[,] greenNoiseData = _greenNoise.Generate(args);

        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                double blueNoiseValue = blueNoiseData[i, j];
                double greenNoiseValue = greenNoiseData[i, j];
                result[i, j] = (_blueNoiseWeight * blueNoiseValue + _greenNoiseWeight * greenNoiseValue) * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Cyan noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}