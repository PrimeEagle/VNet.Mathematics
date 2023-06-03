// ReSharper disable UnusedMember.Global

namespace VNet.Mathematics.Randomization.Noise.Other;
// Voronoi noise, also known as Worley noise or cellular noise, is a type of coherent noise that can be used to generate procedural textures.
// The simplest way of implementing Voronoi noise is by using a Voronoi tessellation algorithm which involves creating a grid of random feature
// points and assigning every point in the space to the nearest feature point.
public class VoronoiNoise : INoiseAlgorithm
{
    private const int pointCount = 30;

    public double[,] Generate(INoiseAlgorithmArgs args)
    {
        var result = new double[args.Height, args.Width];
        var featurePoints = new List<(int X, int Y)>();

        for (int i = 0; i < pointCount; i++)
        {
            var x = args.RandomDistributionAlgorithm.Next(0, args.Width);
            var y = args.RandomDistributionAlgorithm.Next(0, args.Height);
            featurePoints.Add((X: x, Y: y));
        }

        for (int i = 0; i < args.Height; i++)
        {
            for (int j = 0; j < args.Width; j++)
            {
                double minDistance = double.MaxValue;
                foreach (var featurePoint in featurePoints)
                {
                    var dx = featurePoint.X - j;
                    var dy = featurePoint.Y - i;
                    var distance = Math.Sqrt(dx * dx + dy * dy);
                    minDistance = Math.Min(minDistance, distance);
                }

                result[i, j] = minDistance / Math.Sqrt(args.Width * args.Width + args.Height * args.Height) * args.Scale;
            }
        }

        return result;
    }
}
