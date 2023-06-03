// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeMadeStatic.Global

using VNet.Mathematics.Filter;

namespace VNet.Mathematics.Randomization.Noise.Color;
// White noise is characterized as having equal intensity at different frequencies, giving it a flat spectral density.
// In the context of random numbers, white noise is typically represented as a sequence of numbers that are statistically
// independent and identically distributed.
public class WhiteNoise : INoiseAlgorithm
{
    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[args.Height, args.Width];

        for (var i = 0; i < args.Height; i++)
        {
            for (var j = 0; j < args.Width; j++)
            {
                var sample = GenerateSingleSample(args);
                result[i, j] = sample;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        var sample = args.RandomDistributionAlgorithm.NextDouble();

        if (args.Filter is not null && args.FilterArgs is not null && args.Filter.IsValid(args.FilterArgs))
        {
            var filteredSamples = args.Filter.Filter(new double[] { sample }, args.FilterArgs);
            if (filteredSamples.Length > 0)
            {
                sample = filteredSamples[0];
            }
        }

        var quantizationLevel = (int)(sample * args.QuantizeLevels);
        sample = (double)quantizationLevel / args.QuantizeLevels;
        sample *= args.Scale;

        return sample;
    }
}