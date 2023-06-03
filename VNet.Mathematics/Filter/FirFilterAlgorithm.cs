// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeMadeStatic.Local
#pragma warning disable CA1822
namespace VNet.Mathematics.Filter;

public class FirFilterAlgorithm : IFilterAlgorithm
{
    public double[] Apply(double[] input, IFilterArgs args)
    {
        var coefficients = args.BandType switch
        {
            BandType.LowPass => MathNet.Filtering.FIR.FirCoefficients.LowPass(args.SampleRate, args.CutoffFrequency),
            BandType.HighPass => MathNet.Filtering.FIR.FirCoefficients.HighPass(args.SampleRate, args.CutoffFrequency),
            BandType.BandPass => MathNet.Filtering.FIR.FirCoefficients.BandPass(args.SampleRate, ((IBandPassFilterArgs)args).LowCutoffFrequency, ((IBandPassFilterArgs)args).HighCutoffFrequency),
            BandType.Notch => MathNet.Filtering.FIR.FirCoefficients.BandStop(args.SampleRate, ((INotchFilterArgs)args).LowCutoffFrequency, ((INotchFilterArgs)args).HighCutoffFrequency),
            _ => throw new ArgumentException("Invalid filter type.")
        };


        // Apply the filter
        

        ApplyWindowFunction(coefficients, args);
        var filter = new MathNet.Filtering.FIR.OnlineFirFilter(coefficients);
        var result = filter.ProcessSamples(input);

        return result;
    }

    public bool IsValid(IFilterArgs args)
    {
        var valid = args.SampleRate > 0;
        if (valid) valid &= args.CutoffFrequency > 0;
        if (valid && args.BandType == BandType.BandPass) valid &= ((IBandPassFilterArgs)args).LowCutoffFrequency > 0;
        if (valid && args.BandType == BandType.BandPass) valid &= ((IBandPassFilterArgs)args).HighCutoffFrequency > 0;
        if (valid && args.BandType == BandType.Notch) valid &= ((INotchFilterArgs)args).LowCutoffFrequency > 0;
        if (valid && args.BandType == BandType.Notch) valid &= ((INotchFilterArgs)args).HighCutoffFrequency > 0;

        return valid;
    }

    private void ApplyWindowFunction(IList<double> coefficients, IFilterArgs args)
    {
        MathNet.Filtering.Windowing.Window window;

        switch (((IFirFilterArgs)args).WindowFunction)
        {
            case WindowFunction.BartlettHann:
                window = new MathNet.Filtering.Windowing.BartlettHannWindow();
                break;
            case WindowFunction.Bartlett:
                window = new MathNet.Filtering.Windowing.BartlettWindow();
                break;
            case WindowFunction.BlackmanHarris:
                window = new MathNet.Filtering.Windowing.BlackmanHarrisWindow();
                break;
            case WindowFunction.BlackmanNuttall:
                window = new MathNet.Filtering.Windowing.BlackmanNuttallWindow();
                break;
            case WindowFunction.Blackman:
                window = new MathNet.Filtering.Windowing.BlackmanWindow();
                break;
            case WindowFunction.Cosine:
                window = new MathNet.Filtering.Windowing.CosineWindow();
                break;
            case WindowFunction.FlatTop:
                window = new MathNet.Filtering.Windowing.FlatTopWindow();
                break;
            case WindowFunction.Gauss:
                window = new MathNet.Filtering.Windowing.GaussWindow(((IFirFilterArgs)args).Sigma);
                break;
            case WindowFunction.Hamming:
                window = new MathNet.Filtering.Windowing.HammingWindow();
                break;
            case WindowFunction.Hann:
                window = new MathNet.Filtering.Windowing.HannWindow();
                break;
            case WindowFunction.Lanczos:
                window = new MathNet.Filtering.Windowing.LanczosWindow();
                break;
            case WindowFunction.Nuttall:
                window = new MathNet.Filtering.Windowing.NuttallWindow();
                break;
            case WindowFunction.Rectangular:
                window = new MathNet.Filtering.Windowing.RectangularWindow();
                break;
            case WindowFunction.Triangular:
                window = new MathNet.Filtering.Windowing.TriangularWindow();
                break;
            case WindowFunction.None:
            default:
                return;
        }

        window.Width = coefficients.Count;
        var windowArr = window.CopyToArray();

        for (var i = 0; i < coefficients.Count; i++) 
            coefficients[i] *= windowArr[i];
    }
}