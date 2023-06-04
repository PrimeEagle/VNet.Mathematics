// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;

// Azure noise, also known as sky-blue noise, is a term used to describe noise with a power spectral density that increases at a
// rate higher than blue noise. It emphasizes higher frequencies more than blue noise.
public class AzureNoise : NoiseBase
{
    private readonly INoiseAlgorithm _blueNoise;
    private readonly INoiseAlgorithm _violetNoise;
    private readonly double _blueNoiseWeight;
    private readonly double _violetNoiseWeight;

    public AzureNoise(double blueNoiseWeight = 0.5, double violetNoiseWeight = 0.5)
    {
        _blueNoise = new BlueNoise();
        _violetNoise = new VioletNoise();
        _blueNoiseWeight = blueNoiseWeight;
        _violetNoiseWeight = violetNoiseWeight;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[args.Height, args.Width];

        var blueNoiseData = _blueNoise.Generate(args);
        var violetNoiseData = _violetNoise.Generate(args);

        for (var i = 0; i < args.Height; i++)
            for (var j = 0; j < args.Width; j++)
            {
                var blueNoiseValue = blueNoiseData[i, j];
                var violetNoiseValue = violetNoiseData[i, j];
                result[i, j] = (_blueNoiseWeight * blueNoiseValue + _violetNoiseWeight * violetNoiseValue) * args.Scale;
            }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Azure noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}