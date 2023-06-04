// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Color;
//  Lemon noise is a term used to describe noise with a power spectral density that increases at a rate higher than white noise.
// It emphasizes higher frequencies more than white noise.
public class LemonNoise : NoiseBase
{
    private INoiseAlgorithm _baseNoise;
    private double _scale;

    public LemonNoise()
    {
        _baseNoise = new WhiteNoise();
        _scale = 1.0;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int args.Width = args.Width;
        int args.Height = args.Height;

        double[,] result = new double[args.Height, args.Width];

        double[,] baseNoiseData = _baseNoise.Generate(args);

        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                double baseNoiseValue = baseNoiseData[i, j];

                // Apply custom transformation to achieve lemon-like characteristics
                double lemonNoiseValue = Math.Abs(baseNoiseValue) < 0.5 ? baseNoiseValue * 2.0 : 1.0 - (baseNoiseValue - 0.5) * 2.0;

                result[i, j] = lemonNoiseValue * _scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Lemon noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}