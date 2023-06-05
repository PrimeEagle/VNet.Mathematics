// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;

// Brownian noise, also known as Brown noise or red noise, is a type of noise signal that has a power spectrum that decreases by 3 dB per
// octave. It is named "Brownian" after the random motion of particles in Brownian motion, which exhibits similar characteristics.
public class BrownianNoise : INoiseAlgorithm
{
    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[Args.Height, Args.Width];
        var random = new Random();

        double previousSample = 0.0;

        for (var i = 0; i < Args.Height; i++)
        {
            for (var j = 0; j < Args.Width; j++)
            {
                var sample = GenerateSingleSample(args, random, previousSample);
                result[i, j] = sample;
                previousSample = sample;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args, Random random, double previousSample)
    {
        var sample = previousSample + (random.NextDouble() * 2.0 - 1.0);

        if (Args.Filter is not null && Args.FilterArgs is not null && Args.Filter.IsValid(Args.FilterArgs))
        {
            var filteredSamples = Args.Filter.Filter(new double[] { sample }, Args.FilterArgs);
            if (filteredSamples.Length > 0)
            {
                sample = filteredSamples[0];
            }
        }

        var quantizationLevel = (int)(sample * Args.QuantizeLevels);
        sample = (double)quantizationLevel / Args.QuantizeLevels;
        sample *= Args.Scale;

        return sample;
    }
}
