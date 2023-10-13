namespace VNet.Mathematics.Randomization.Generation;

public interface IRandomGenerationAlgorithm : IRandomizationAlgorithm
{
    public int Next();
    public long NextLong();
    public float NextSingle();
    public double NextDouble();
    public int Next(int maxValue);
    public long NextLong(long maxValue);
    public float NextSingle(float maxValue);
    public double NextDouble(double maxValue);
    public int Next(int minValue, int maxValue);
    public long NextLong(long minValue, long maxValue);
    public float NextSingle(float minValue, float maxValue);
    public double NextDouble(double minValue, double maxValue);
    public int NextInclusive(int minValue, int maxValue);
    public long NextLongInclusive(long minValue, long maxValue);
    public float NextSingleInclusive(float minValue, float maxValue);
    public double NextDoubleInclusive(double minValue, double maxValue);
}