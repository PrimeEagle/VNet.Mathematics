// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Digital noise refers to noise that is introduced during digital signal processing or data acquisition. It can occur due to quantization errors,
// sampling limitations, or processing artifacts. Digital noise can manifest as random fluctuations, quantization noise, or other distortions in digital signals.
public class DigitalNoise : INoiseAlgorithm
{
    private int _quantizeLevels;

    public DigitalNoise(int quantizeLevels = 256)
    {
        _quantizeLevels = quantizeLevels;
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
                int quantizedValue = (int)(randomValue * _quantizeLevels);
                double scaledValue = quantizedValue / (double)(_quantizeLevels - 1);
                result[i, j] = scaledValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Digital noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}