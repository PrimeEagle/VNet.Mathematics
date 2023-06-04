// ReSharper disable UnusedMember.Global

using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using VNet.Mathematics.Filter;

namespace VNet.Mathematics.Randomization.Noise.Color;
// Red noise, also known as Brown or Brownian noise, is similar to pink noise in that it also decreases in power as frequency increases.However,
// while pink noise decreases power by 3 dB per octave, red noise decreases power by 6 dB per octave (or 20 dB per decade). This means it's essentially
// "more pink" than pink noise. Generating red noise is typically done by integrating white noise, or in other words, each sample of red noise is
// the sum of the current white noise sample and the previous red noise sample.
public class RedNoise : NoiseBase
{
    private readonly WhiteNoise _whiteNoise;
    private double _lastSample = 0;

    public RedNoise()
    {
        _whiteNoise = new WhiteNoise();
    }

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
        var whiteNoiseSample = _whiteNoise.GenerateSingleSample(args);
        var sample = _lastSample + whiteNoiseSample;

        args.FilterArgs = new FilterParameters()
        {
            SamplingRate = 44100, // Assuming the sample rate is 44100 samples per second
            CutoffFrequency = 5000, // A cutoff frequency of 5000 Hz is a common choice for pink noise
            Order = 1, // A first order filter for simplicity
            FilterType = FilterType.IIR,
            BandType = BandType.LowPass, // Pink noise is low-pass in nature
            WindowFunction = WindowFunction.None // Window function is typically not used with IIR filters
        };

        args.OutputFilter = new LowPassFilter(new IirFilterAlgorithm());

        if (args.OutputFilter is not null && args.FilterArgs is not null && args.OutputFilter.IsValid(args.FilterArgs))
        {
            var filteredSamples = args.OutputFilter.Filter(new double[] { sample }, args.FilterArgs);
            if (filteredSamples.Length > 0)
            {
                sample = filteredSamples[0];
            }
        }

        var quantizationLevel = (int)(sample * args.QuantizeLevels);
        sample = (double)quantizationLevel / args.QuantizeLevels;
        sample *= args.Scale;

        _lastSample = sample;

        return sample;
    }
}

