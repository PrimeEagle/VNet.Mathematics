// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;

public class BlackNoise : NoiseBase
{
    private INoiseAlgorithm _whiteNoise;

    public BlackNoise()
    {
        _whiteNoise = new WhiteNoise();
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var whiteNoiseData = _whiteNoise.Generate(args);

        var result = new double[args.Height, args.Width];
        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                var whiteNoiseValue = whiteNoiseData[i, j];
                result[i, j] = whiteNoiseValue * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Black noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}