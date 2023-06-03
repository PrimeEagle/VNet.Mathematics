// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
// Pinky noise is a variation of pink noise that exhibits a more natural, organic texture. It is generated using a combination of fractal
// algorithms, filtering techniques, or a combination of other noise types. Pinky noise is used in audio synthesis, sound design, and
// generating natural soundscapes.
public class BlackNoise : INoiseAlgorithm
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