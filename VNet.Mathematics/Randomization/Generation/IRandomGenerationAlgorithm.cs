using VNet.Configuration;


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
    public int Next(VNet.Configuration.Range<int> range);
    public long NextLong(VNet.Configuration.Range<int> range);
    public float NextSingle(VNet.Configuration.Range<int> range);
    public double NextDouble(VNet.Configuration.Range<int> range);
    public int NextInclusive(VNet.Configuration.Range<int> range);
    public long NextLongInclusive(VNet.Configuration.Range<int> range);
    public float NextSingleInclusive(VNet.Configuration.Range<int> range);
    public double NextDoubleInclusive(VNet.Configuration.Range<int> range);
}