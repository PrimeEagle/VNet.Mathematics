// ReSharper disable UnusedMember.Global

using VNet.Mathematics.Filter;
using VNet.Mathematics.Randomization.Distribution.Continuous;

namespace VNet.Mathematics.Randomization.Noise.Color;

// Violet noise, also known as differentiated white noise, is characterized by a power spectral density that increases 6 dB per octave with
// increasing frequency (density proportional to f^2) over a finite frequency range. In other words, it has more power at higher frequencies.
// To generate violet noise, we can first generate white noise and then apply a high-pass filter or differentiation process.
public class VioletNoise : INoiseAlgorithm
{
    private readonly WhiteNoise _whiteNoise;
    private readonly double[] _state;

    public VioletNoise()
    {
        _whiteNoise = new WhiteNoise();
        _state = new double[1]; // Assuming that the filter will be an IIR filter of order 1.
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[args.Height, args.Width];

        for (var i = 0; i < args.Height; i++)
            for (var j = 0; j < args.Width; j++)
            {
                var sample = GenerateSingleSample(args);
                result[i, j] = sample;
            }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        var sample = _whiteNoise.GenerateSingleSample(args);

        args.FilterArgs = new FilterParameters()
        {
            SamplingRate = 44100,                     // Assuming the sample rate is 44100 samples per second
            CutoffFrequency = 5000,                 // A cutoff frequency of 5000 Hz is a common choice for pink noise
            Order = 1,                              // A first order filter for simplicity
            FilterType = FilterType.IIR,
            BandType = BandType.HighPass,           // Pink noise is low-pass in nature
            WindowFunction = WindowFunction.None    // Window function is typically not used with IIR filters
        };
        args.OutputFilter = new HighPassFilter(new IirFilterAlgorithm());

        if (args.OutputFilter is not null && args.FilterArgs is not null && args.OutputFilter.IsValid(args.FilterArgs))
        {
            _state[0] = sample;
            var filteredSamples = args.OutputFilter.Filter(_state, args.FilterArgs);
            if (filteredSamples.Length > 0) sample = filteredSamples[0];
        }

        var quantizationLevel = (int)(sample * args.QuantizeLevels);
        sample = (double)quantizationLevel / args.QuantizeLevels;
        sample *= args.Scale;

        return sample;
    }
}