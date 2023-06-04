// ReSharper disable UnusedMember.Global

using System.Numerics;
using VNet.Mathematics.Filter;

namespace VNet.Mathematics.Randomization.Noise.Color;

public class BlueNoise : INoiseAlgorithm
{
    private double _radius;
    private int _maxAttempts;

    public BlueNoise(double radius = 0.1, int maxAttempts = 30)
    {
        _radius = radius;
        _maxAttempts = maxAttempts;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int args.Width = args.Width;
        int args.Height = args.Height;

        List<Vector2> samples = GenerateBlueNoiseSamples(args.Width, args.Height);

        double[,] result = new double[args.Height, args.Width];
        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                Vector2 samplePoint = new Vector2((float)j / args.Width, (float)i / args.Height);
                double minDistance = double.MaxValue;

                foreach (Vector2 sample in samples)
                {
                    double distance = Vector2.Distance(samplePoint, sample);
                    minDistance = Math.Min(minDistance, distance);
                }

                result[i, j] = minDistance * args.Scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Blue noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private List<Vector2> GenerateBlueNoiseSamples(int args.Width, int args.Height)
    {
        double cellSize = _radius / Math.Sqrt(2);
        int gridWidth = (int)Math.Ceiling(args.Width / cellSize);
        int gridHeight = (int)Math.Ceiling(args.Height / cellSize);

        int[,] grid = new int[gridHeight, gridWidth];
        List<Vector2> samples = new List<Vector2>();
        List<Vector2> activeSamples = new List<Vector2>();

        Random random = new Random();
        Vector2 firstSample = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
        activeSamples.Add(firstSample);
        samples.Add(firstSample);
        int gridX = (int)(firstSample.X * gridWidth);
        int gridY = (int)(firstSample.Y * gridHeight);
        grid[gridY, gridX] = 1;

        while (activeSamples.Count > 0)
        {
            int index = random.Next(activeSamples.Count);
            Vector2 sample = activeSamples[index];
            bool foundCandidate = false;

            for (int attempt = 0; attempt < _maxAttempts; attempt++)
            {
                double angle = 2 * Math.PI * random.NextDouble();
                double distance = _radius * (1 + random.NextDouble());
                Vector2 candidate = sample + new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle)));

                if (candidate.X >= 0 && candidate.X < 1 && candidate.Y >= 0 && candidate.Y < 1 && IsCandidateValid(candidate, samples, grid, cellSize, gridWidth, gridHeight))
                {
                    activeSamples.Add(candidate);
                    samples.Add(candidate);
                    gridX = (int)(candidate.X * gridWidth);
                    gridY = (int)(candidate.Y * gridHeight);
                    grid[gridY, gridX] = 1;
                    foundCandidate = true;
                    break;
                }
            }

            if (!foundCandidate)
            {
                activeSamples.RemoveAt(index);
            }
        }

        return samples;
    }

    private bool IsCandidateValid(Vector2 candidate, List<Vector2> samples, int[,] grid, double cellSize, int gridWidth, int gridHeight)
    {
        int cellX = (int)(candidate.X * gridWidth);
        int cellY = (int)(candidate.Y * gridHeight);

        int startX = Math.Max(0, cellX - 2);
        int startY = Math.Max(0, cellY - 2);
        int endX = Math.Min(gridWidth - 1, cellX + 2);
        int endY = Math.Min(gridHeight - 1, cellY + 2);

        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                if (grid[y, x] == 1)
                {
                    Vector2 existingSample = samples[grid[y, x] - 1];
                    double distance = Vector2.Distance(candidate, existingSample);

                    if (distance < cellSize)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}