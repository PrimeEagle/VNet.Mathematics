// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// The Diamond-Square algorithm is not a distinct type of noise but rather an algorithm used to generate fractal terrain or heightmaps.
// It is a method for generating terrain-like patterns with realistic variations. The algorithm starts with a square grid where each corner
// of the grid is given an initial height value.The algorithm then iteratively subdivides the squares into smaller squares and performs a
// diamond-square step.In the diamond step, the algorithm calculates the average height of the four corner points and adds a random displacement.
// In the square step, the algorithm calculates the average height of the four midpoints of the sides and adds a random displacement.
// By repeating this process for each iteration and adjusting the random displacements, the Diamond-Square algorithm generates a fractal-like
// pattern that resembles terrain.It creates a smooth, continuous, and self-similar structure with details at different scales.
public class DiamondSquareNoise : INoiseAlgorithm
{
    private double _roughness;
    private double _scale;

    public DiamondSquareNoise(double roughness = 0.5, double scale = 1.0)
    {
        _roughness = roughness;
        _scale = scale;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = args.Width;
        int height = args.Height;

        // Ensure the grid size is a power of 2 plus 1
        int gridSize = GetNextPowerOfTwo(Math.Max(width, height)) + 1;

        double[,] grid = new double[gridSize, gridSize];
        grid[0, 0] = RandomRange(-1.0, 1.0);
        grid[0, gridSize - 1] = RandomRange(-1.0, 1.0);
        grid[gridSize - 1, 0] = RandomRange(-1.0, 1.0);
        grid[gridSize - 1, gridSize - 1] = RandomRange(-1.0, 1.0);

        DiamondSquare(grid, 0, 0, gridSize - 1, gridSize - 1, _roughness);

        double[,] result = new double[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int x = (int)Math.Floor(j * (double)(gridSize - 1) / (width - 1));
                int y = (int)Math.Floor(i * (double)(gridSize - 1) / (height - 1));
                result[i, j] = grid[y, x] * _scale;
            }
        }

        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Diamond-Square noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private void DiamondSquare(double[,] grid, int startX, int startY, int endX, int endY, double roughness)
    {
        int size = endX - startX;
        int halfSize = size / 2;

        if (halfSize < 1)
            return;

        double scale = roughness * size;

        // Diamond step
        for (int y = startY + halfSize; y < endY; y += size)
        {
            for (int x = startX + halfSize; x < endX; x += size)
            {
                double a = grid[y - halfSize, x - halfSize];
                double b = grid[y - halfSize, x + halfSize];
                double c = grid[y + halfSize, x - halfSize];
                double d = grid[y + halfSize, x + halfSize];
                double average = (a + b + c + d) / 4.0;
                double offset = RandomRange(-scale, scale);
                grid[y, x] = average + offset;
            }
        }

        // Square step
        for (int y = startY; y <= endY; y += halfSize)
        {
            for (int x = startX + (y + halfSize) % size; x <= endX; x += size)
            {
                double a = GetGridValue(grid, x - halfSize, y);
                double b = GetGridValue(grid, x + halfSize, y);
                double c = GetGridValue(grid, x, y - halfSize);
                double d = GetGridValue(grid, x, y + halfSize);
                double average = (a + b + c + d) / 4.0;
                double offset = RandomRange(-scale, scale);
                grid[y, x] = average + offset;
            }
        }

        DiamondSquare(grid, startX, startY, startX + halfSize, startY + halfSize, roughness);
        DiamondSquare(grid, startX + halfSize, startY, endX, startY + halfSize, roughness);
        DiamondSquare(grid, startX, startY + halfSize, startX + halfSize, endY, roughness);
        DiamondSquare(grid, startX + halfSize, startY + halfSize, endX, endY, roughness);
    }

    private double GetGridValue(double[,] grid, int x, int y)
    {
        int gridSize = grid.GetLength(0);
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
            return 0.0;
        return grid[y, x];
    }

    private double RandomRange(double min, double max)
    {
        return min + (max - min) * RandomProvider.NextDouble();
    }

    private int GetNextPowerOfTwo(int n)
    {
        int power = 1;
        while (power < n)
            power *= 2;
        return power;
    }
}
