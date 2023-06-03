// ReSharper disable UnusedMember.Global

using VNet.Mathematics.Randomization.Distribution;

namespace VNet.Mathematics.Randomization.Noise.Other;
// Exponential noise is a type of noise that follows an exponential distribution. It is characterized by a rapid initial decay and a
// long tail. Exponential noise can be used to model phenomena with decay or exponential growth processes.
public class ExponentialNoise : INoiseAlgorithm
{
    private double _lambda;

    public ExponentialNoise(double lambda = 1.0)
    {
        _lambda = lambda;
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
                double randomValue = GenerateExponentialRandomValue(_lambda, args.RandomDistributionAlgorithm);
                result[i, j] = randomValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Exponential noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private double GenerateExponentialRandomValue(double lambda, IRandomDistributionAlgorithm randomDistributionAlgorithm)
    {
        double uniformRandomValue = randomDistributionAlgorithm.NextDouble();
        return -Math.Log(1 - uniformRandomValue) / lambda;
    }
}