// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// The term "brown noise" can also be used in non-audio contexts to describe noise with a power spectral density that decreases as the frequency increases.
public class BrownNoise : INoiseAlgorithm
{
    private double _previousValue;
    private double _scale;

    public BrownNoise(double scale = 1.0)
    {
        _previousValue = 0.0;
        _scale = scale;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double randomValue = args.RandomDistributionAlgorithm.NextDouble();
                _previousValue += (randomValue - 0.5) * 2.0;
                result[i, j] = _previousValue * _scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Brown noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}