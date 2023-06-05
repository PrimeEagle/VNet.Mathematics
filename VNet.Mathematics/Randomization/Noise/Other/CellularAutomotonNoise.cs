// ReSharper disable UnusedMember.Global

using VNet.Mathematics.Randomization.Distribution;

namespace VNet.Mathematics.Randomization.Noise.Other;
// Cellular automaton algorithms divide space into discrete cells and update their states based on predefined rules. The resulting noise
// exhibits emergent behaviors and complex patterns. Cellular automata have applications in generating natural textures, simulating systems,
// and creating procedural content.
public class CellularAutomatonNoise : INoiseAlgorithm
{
    private int _iterations;
    private double _threshold;

    public CellularAutomatonNoise(int iterations = 5, double threshold = 0.5)
    {
        _iterations = iterations;
        _threshold = threshold;
    }

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        int width = Args.Width;
        int height = Args.Height;

        double[,] result = new double[height, width];
        int[,] grid = new int[height, width];

        // Initialize the grid with random values
        InitializeGrid(grid, Args.RandomDistributionAlgorithm);

        // Perform the cellular automaton simulation
        for (int i = 0; i < _iterations; i++)
        {
            grid = ApplyCellularAutomatonRule(grid);
        }

        // Convert the binary grid to noise values
        ConvertGridToNoise(grid, result, Args.QuantizeLevels, Args.Scale);
        return result;
    }

    public double GenerateSingleSample(INoiseAlgorithmArgs args)
    {
        // Cellular automaton noise is generated for the entire grid, so generating a single sample is not applicable.
        throw new NotImplementedException();
    }

    private void InitializeGrid(int[,] grid, IRandomDistributionAlgorithm randomDistributionAlgorithm)
    {
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[i, j] = randomDistributionAlgorithm.NextDouble() < _threshold ? 1 : 0;
            }
        }
    }

    private int[,] ApplyCellularAutomatonRule(int[,] grid)
    {
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        int[,] newGrid = new int[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int state = grid[i, j];
                int neighbors = CountAliveNeighbors(grid, i, j);

                if (state == 0 && neighbors >= 5)
                {
                    newGrid[i, j] = 1;
                }
                else if (state == 1 && neighbors <= 3)
                {
                    newGrid[i, j] = 0;
                }
                else
                {
                    newGrid[i, j] = state;
                }
            }
        }

        return newGrid;
    }

    private int CountAliveNeighbors(int[,] grid, int x, int y)
    {
        int count = 0;
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = (x + i + height) % height;
                int neighborY = (y + j + width) % width;
                count += grid[neighborX, neighborY];
            }
        }

        count -= grid[x, y];
        return count;
    }

    private void ConvertGridToNoise(int[,] grid, double[,] noise, int quantizeLevels, double scale)
    {
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                double value = grid[i, j] == 1 ? 1.0 : 0.0;
                noise[i, j] = value * (quantizeLevels - 1) * scale;
            }
        }
    }
}
