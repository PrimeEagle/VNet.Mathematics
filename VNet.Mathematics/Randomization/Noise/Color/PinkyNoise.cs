// ReSharper disable UnusedMember.Global

using VNet.Mathematics.Randomization.Distribution;

namespace VNet.Mathematics.Randomization.Noise.Color;
// Pinky noise is a variation of pink noise that exhibits a more natural, organic texture. It is generated using a combination of fractal
// algorithms, filtering techniques, or a combination of other noise types. Pinky noise is used in audio synthesis, sound design, and
// generating natural soundscapes.
public class PinkyNoise : INoiseAlgorithm
{
    private int _numSteps;
    private double _stepSize;

    public PinkyNoise(int numSteps = 1000, double stepSize = 0.1)
    {
        _numSteps = numSteps;
        _stepSize = stepSize;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        double[,] result = new double[height, width];
        double[,] whiteNoise = GenerateWhiteNoise(width, height, args.RandomDistributionAlgorithm);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double sample = whiteNoise[i, j];

                for (int k = 1; k <= _numSteps; k++)
                {
                    double randomStep = (2 * args.RandomDistributionAlgorithm.NextDouble() - 1) * _stepSize;
                    sample += randomStep / (k * k);
                }

                result[i, j] = sample * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Pinky noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private double[,] GenerateWhiteNoise(int width, int height, IRandomDistributionAlgorithm randomDistributionAlgorithm)
    {
        double[,] noise = new double[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                noise[i, j] = randomDistributionAlgorithm.NextDouble();
            }
        }

        return noise;
    }
}
