// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise;

public class OrangeNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _blueNoise;
    private INoiseAlgorithm _whiteNoise;
    private INoiseAlgorithm _grayNoise;
    private double _blueNoiseWeight;
    private double _whiteNoiseWeight;
    private double _grayNoiseWeight;

    public OrangeNoise(double blueNoiseWeight = 0.5, double whiteNoiseWeight = 0.3, double grayNoiseWeight = 0.2)
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
        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                var blueNoiseValue = blueNoiseData[i, j];
                var whiteNoiseValue = whiteNoiseData[i, j];
                var grayNoiseValue = grayNoiseData[i, j];

                var orangeNoiseValue = (_blueNoiseWeight * blueNoiseValue) + (_whiteNoiseWeight * whiteNoiseValue) + (_grayNoiseWeight * grayNoiseValue);
                result[i, j] = orangeNoiseValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Orange noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}