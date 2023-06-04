// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;

public class YellowNoise : NoiseBase
{
    private readonly INoiseAlgorithm _blueNoise;
    private readonly INoiseAlgorithm _whiteNoise;
    private readonly INoiseAlgorithm _grayNoise;
    private readonly double _blueNoiseWeight;
    private readonly double _whiteNoiseWeight;
    private readonly double _grayNoiseWeight;

    public YellowNoise(double blueNoiseWeight = 0.7, double whiteNoiseWeight = 0.3, double grayNoiseWeight = 0.0)
    {
        _blueNoise = new BlueNoise();
        _whiteNoise = new WhiteNoise();
        _grayNoise = new GrayNoise();
        _blueNoiseWeight = blueNoiseWeight;
        _whiteNoiseWeight = whiteNoiseWeight;
        _grayNoiseWeight = grayNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var blueNoiseData = _blueNoise.Generate(args);
        var whiteNoiseData = _whiteNoise.Generate(args);
        var grayNoiseData = _grayNoise.Generate(args);

        var result = new double[args.Height, args.Width];
        for (var i = 0; i < args.Height; i++)
            for (var j = 0; j < args.Width; j++)
            {
                var blueNoiseValue = blueNoiseData[i, j];
                var whiteNoiseValue = whiteNoiseData[i, j];
                var grayNoiseValue = grayNoiseData[i, j];

                var yellowNoiseValue = _blueNoiseWeight * blueNoiseValue + _whiteNoiseWeight * whiteNoiseValue + _grayNoiseWeight * grayNoiseValue;
                result[i, j] = yellowNoiseValue * args.Scale;
            }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Yellow noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}