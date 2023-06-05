﻿// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Harmonic noise is characterized by the presence of noise components that are harmonically related to a fundamental frequency.
// It can arise from nonlinear distortion or interference in electrical or audio systems. Harmonic noise can be generated by introducing
// noise at specific harmonic frequencies.
public class HarmonicNoise : INoiseAlgorithm
{
    private double[] _frequencies;
    private double[] _amplitudes;

    public HarmonicNoise(double[] frequencies, double[] amplitudes)
    {
        _frequencies = frequencies;
        _amplitudes = amplitudes;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = Args.Width;
        int height = Args.Height;
        int numHarmonics = _frequencies.Length;

        double[,] result = new double[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double noiseValue = 0.0;

                for (int h = 0; h < numHarmonics; h++)
                {
                    double frequency = _frequencies[h];
                    double amplitude = _amplitudes[h];
                    double phase = 2.0 * Math.PI * frequency * (i + j) / Args.SampleRate;

                    noiseValue += amplitude * Math.Sin(phase);
                }

                result[i, j] = noiseValue * Args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Harmonic noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}