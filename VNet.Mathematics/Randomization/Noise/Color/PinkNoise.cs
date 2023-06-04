// ReSharper disable UnusedMember.Global

using VNet.Mathematics.Filter;

namespace VNet.Mathematics.Randomization.Noise.Color;

// Pink noise, also known as 1/f noise, has equal energy in all octaves (or similar log bundles) of frequency. In terms of power at a constant
// bandwidth, pink noise falls off at 3 dB per octave. Generating pink noise is more complicated than white or violet noise, as it requires more
// advanced signal processing techniques.A common method to generate pink noise is using IIR (Infinite Impulse Response) filters, but it
// involves quite a complex logic. Here is a simple approximation in C#, using the Stochastic Difference Equation method, often known as the
// IIR Filter method, or the Stochastic Difference Equation.
public class PinkNoise : NoiseBase
{
    private readonly WhiteNoise _whiteNoise;
    private double[] _state;

    public PinkNoise(INoiseAlgorithmArgs args):base(args)
    {
        _whiteNoise = new WhiteNoise();
        _state = new double[1]; // Assuming that the filter will be an IIR filter of order 1.
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
        var sample = _whiteNoise.GenerateSingleSample(args);

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
            _state[0] = sample; // The new white noise sample is added to the state.
            var filteredSamples = args.OutputFilter.Filter(_state, args.FilterArgs);
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
