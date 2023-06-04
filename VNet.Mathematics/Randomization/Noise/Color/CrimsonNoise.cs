// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Crimson noise is a term occasionally used to describe noise with a power spectral density that increases at a rate higher than
// red (brown) noise. It emphasizes higher frequencies more than red noise.
public class CrimsonNoise : INoiseAlgorithm
{
    private INoiseAlgorithm _redNoise;
    private INoiseAlgorithm _purpleNoise;
    private double _redNoiseWeight;
    private double _purpleNoiseWeight;

    public CrimsonNoise(double redNoiseWeight = 0.5, double purpleNoiseWeight = 0.5)
    {
        _redNoise = new RedNoise();
        _purpleNoise = new PurpleNoise();
        _redNoiseWeight = redNoiseWeight;
        _purpleNoiseWeight = purpleNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int args.Width = args.Width;
        int args.Height = args.Height;

        double[,] result = new double[args.Height, args.Width];

        double[,] redNoiseData = _redNoise.Generate(args);
        double[,] purpleNoiseData = _purpleNoise.Generate(args);

        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                double redNoiseValue = redNoiseData[i, j];
                double purpleNoiseValue = purpleNoiseData[i, j];
                result[i, j] = (_redNoiseWeight * redNoiseValue + _purpleNoiseWeight * purpleNoiseValue) * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Crimson noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}