﻿// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Salt and pepper noise, also known as impulse noise, is a type of noise that appears as random black and white pixels in an image or signal.
// It is typically caused by errors in transmission or data corruption. Salt and pepper noise can be generated by randomly replacing pixels with
// maximum or minimum intensity values.
public class SaltAndPepperNoise : INoiseAlgorithm
{
    private double _density;
    private double _saltValue;
    private double _pepperValue;

    public SaltAndPepperNoise(double density = 0.05, double saltValue = 1.0, double pepperValue = 0.0)
    {
        _density = density;
        _saltValue = saltValue;
        _pepperValue = pepperValue;
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
                if (randomValue < _density)
                {
                    double saltOrPepper = args.RandomDistributionAlgorithm.NextDouble();
                    result[i, j] = saltOrPepper < 0.5 ? _saltValue * args.Scale : _pepperValue * args.Scale;
                }
                else
                {
                    result[i, j] = args.Scale * args.RandomDistributionAlgorithm.NextDouble();
                }
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Salt and pepper noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}