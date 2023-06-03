// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Fractal noise, also known as Fractional Brownian Motion (fBm), is a way of combining multiple layers of noise to create a more complex and
// rich output. Each layer of noise is typically called an "octave", and each octave is given a frequency and an amplitude.
public class FractalNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _baseNoise;
    private int _octaves;
    private double _lacunarity;
    private double _gain;

    public FractalNoise(INoiseAlgorithm baseNoise, int octaves = 8, double lacunarity = 2.0, double gain = 0.5)
    {
        _baseNoise = baseNoise;
        _octaves = octaves;
        _lacunarity = lacunarity;
        _gain = gain;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[args.Height, args.Width];

        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                double frequency = 1;
                double amplitude = 1;
                double total = 0;

                for (int octave = 0; octave < _octaves; octave++)
                {
                    double x = j * frequency / args.Width;
                    double y = i * frequency / args.Height;
                    double noise = _baseNoise.GenerateSingleSample(new NoiseAlgorithmArgs
                    {
                        Width = (int)x,
                        Height = (int)y,
                        QuantizeLevels = args.QuantizeLevels,
                        Scale = args.Scale,
                        RandomDistributionAlgorithm = args.RandomDistributionAlgorithm
                    });

                    total += noise * amplitude;
                    frequency *= _lacunarity;
                    amplitude *= _gain;
                }

                result[i, j] = total;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Fractal noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}