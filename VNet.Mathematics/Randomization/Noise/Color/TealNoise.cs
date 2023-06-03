// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Teal noise is a term used to describe noise with a power spectral density that increases at a rate higher than blue noise.
// It emphasizes higher frequencies more than blue noise.
public class TealNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _baseNoise;
    private double _scale;

    public TealNoise()
    {
        _baseNoise = new WhiteNoise();
        _scale = 1.0;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];

        double[,] baseNoiseData = _baseNoise.Generate(args);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double baseNoiseValue = baseNoiseData[i, j];

                // Apply custom transformation to achieve teal-like characteristics
                double tealNoiseValue = Math.Sin(2.0 * Math.PI * baseNoiseValue);

                result[i, j] = tealNoiseValue * _scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Teal noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}