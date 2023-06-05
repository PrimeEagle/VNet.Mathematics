// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Delta noise, also known as Dirac noise or impulse noise, represents an instantaneous, infinitely short noise burst. It is
// often used to simulate or model abrupt or transient events in a signal.
public class DeltaNoise : INoiseAlgorithm
{
    private double _deltaValue;
    private int _deltaIndex;

    public DeltaNoise(double deltaValue = 1.0, int deltaIndex = 0)
    {
        _deltaValue = deltaValue;
        _deltaIndex = deltaIndex;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = Args.Width;
        int height = Args.Height;

        double[,] result = new double[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                result[i, j] = i == _deltaIndex && j == _deltaIndex ? _deltaValue * Args.Scale : 0.0;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Delta noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }
}