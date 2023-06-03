// ReSharper disable UnusedMember.Global
namespace VNet.Mathematics.Filter;

public class IirFilterAlgorithm : IFilterAlgorithm
{
    public double[] Apply(double[] input, IFilterArgs args)
    {
        var coefficients = args.BandType switch
        {
            BandType.LowPass => MathNet.Filtering.IIR.IirCoefficients.LowPass(args.SampleRate, args.CutoffFrequency, args.Bandwidth),
            BandType.HighPass => MathNet.Filtering.IIR.IirCoefficients.HighPass(args.SampleRate, args.CutoffFrequency, args.Bandwidth),
            BandType.BandPass => MathNet.Filtering.IIR.IirCoefficients.BandPass(args.SampleRate, args.CutoffFrequency, args.Bandwidth),
            BandType.Notch => MathNet.Filtering.IIR.IirCoefficients.BandStop(args.SampleRate, args.CutoffFrequency, args.Bandwidth),
            _ => throw new ArgumentException("Invalid filter type.")
        };

        // Apply the filter
        var filter = new MathNet.Filtering.IIR.OnlineIirFilter(coefficients);

        var result = filter.ProcessSamples(input);

        return result;
    }

    public bool IsValid(IFilterArgs args)
    {
        var valid = args.SampleRate > 0;
        if (valid) valid &= args.CutoffFrequency > 0;
        if (valid) valid &= ((IIirFilterParameters)args).Bandwidth > 0;

        return valid;
    }
}