// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Chirp noise is a type of noise signal that exhibits a frequency sweep over time. It starts at a low frequency and gradually increases or
// decreases in frequency. Chirp noise is used in various applications, including audio testing, sonar systems, and signal processing experiments.
public class ChirpNoise : INoiseAlgorithm
{
    private double _startFrequency;
    private double _endFrequency;
    private double _duration;
    private INoiseAlgorithm _baseNoise;

    public ChirpNoise(double startFrequency, double endFrequency, double duration, INoiseAlgorithm baseNoise)
    {
        _startFrequency = startFrequency;
        _endFrequency = endFrequency;
        _duration = duration;
        _baseNoise = baseNoise;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = Args.Width;
        int height = Args.Height;
        double sampleRate = Args.SampleRate;

        double[,] result = new double[height, width];
        double timeStep = 1.0 / sampleRate;

        for (int i = 0; i < height; i++)
        {
            double time = 0.0;
            double frequency = _startFrequency;

            for (int j = 0; j < width; j++)
            {
                double noiseValue = _baseNoise.GenerateSingleSample(args);
                result[i, j] = noiseValue * Math.Sin(2 * Math.PI * frequency * time);
                time += timeStep;
                frequency = GetChirpFrequency(time);
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Chirp noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private double GetChirpFrequency(double time)
    {
        double t = time / _duration;
        return _startFrequency + (_endFrequency - _startFrequency) * t;
    }
}
