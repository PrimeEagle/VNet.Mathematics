// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;

public class GreenNoise : NoiseBase
{
    private INoiseAlgorithm _blueNoise;

    public GreenNoise()
    {
        _blueNoise = new BlueNoise();
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var blueNoiseData = _blueNoise.Generate(args);

        var result = new double[args.Height, args.Width];
        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                var blueNoiseValue = blueNoiseData[i, j];
                result[i, j] = blueNoiseValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Green noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}