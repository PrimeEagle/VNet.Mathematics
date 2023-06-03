// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;

// Gray noise is a variation of pink noise with a more balanced power spectral density distribution across the frequency spectrum.It is designed to have an equal
// amount of energy in each octave, resulting in a pleasing and natural sound.
public class GrayNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _blueNoise;
    private INoiseAlgorithm _whiteNoise;
    private double _blueNoiseWeight;
    private double _whiteNoiseWeight;

    public GrayNoise(double blueNoiseWeight = 0.5, double whiteNoiseWeight = 0.5)
    {
        _blueNoise = new BlueNoise();
        _whiteNoise = new WhiteNoise();
        _blueNoiseWeight = blueNoiseWeight;
        _whiteNoiseWeight = whiteNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var blueNoiseData = _blueNoise.Generate(args);
        var whiteNoiseData = _whiteNoise.Generate(args);

        var result = new double[args.Height, args.Width];
        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                var blueNoiseValue = blueNoiseData[i, j];
                var whiteNoiseValue = whiteNoiseData[i, j];

                var grayNoiseValue = _blueNoiseWeight * blueNoiseValue + _whiteNoiseWeight * whiteNoiseValue;
                result[i, j] = grayNoiseValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Gray noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}