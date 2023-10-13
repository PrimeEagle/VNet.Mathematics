// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable CompareOfFloatsByEqualityOperator
#pragma warning disable CA1822

namespace VNet.Mathematics.Randomization.Generation;

public abstract class RandomGenerationBase : IRandomGenerationAlgorithm
{
    protected List<double> Seeds { get; init; }
    protected readonly object Lock = new();



    public abstract int Next();

    public long NextLong()
    {
        var lowBits = Next();
        var highBits = Next();

        return ((long)highBits << 32) | (uint)lowBits;
    }

    public float NextSingle()
    {
        return (Next() & 0x7FFFFFFF) * (1.0f / 0x7FFFFFFF);
    }

    public double NextDouble()
    {
        return ((long)(Next() & 0x000FFFFF) << 32 | (uint)Next()) * (1.0 / (long)0x20000000000000);
    }

    public int Next(int maxValue)
    {
        var mask = int.MaxValue - int.MaxValue % maxValue;

        while (true)
        {
            var value = Next();
            if (value < mask)
                return value % maxValue;
        }
    }

    public long NextLong(long maxValue)
    {
        var mask = long.MaxValue - long.MaxValue % maxValue;

        while (true)
        {
            var value = NextLong();
            if (value < mask)
                return value % maxValue;
        }
    }
    
    public float NextSingle(float maxValue)
    {
        var mask = float.MaxValue - float.MaxValue % maxValue;

        while (true)
        {
            var value = NextSingle();
            if (value < mask)
                return value % maxValue;
        }
    }
    
    public double NextDouble(double maxValue)
    {
        var mask = double.MaxValue - double.MaxValue % maxValue;

        while (true)
        {
            var value = NextDouble();
            if (value < mask)
                return value % maxValue;
        }
    }

    public int Next(int minValue, int maxValue)
    {
        return minValue + Next(maxValue - minValue);
    }

    public long NextLong(long minValue, long maxValue)
    {
        return minValue + NextLong(maxValue - minValue);
    }

    public float NextSingle(float minValue, float maxValue)
    {
        return minValue + NextSingle() * (maxValue - minValue);
    }

    public double NextDouble(double minValue, double maxValue)
    {
        return minValue + NextDouble() * (maxValue - minValue);
    }

    public int NextInclusive(int minValue, int maxValue)
    {
        return Next(minValue, maxValue + 1);
    }

    public long NextLongInclusive(long minValue, long maxValue)
    {
        return NextLong(minValue, maxValue + 1);
    }

    public float NextSingleInclusive(float minValue, float maxValue)
    {
        while (true)
        {
            var result = NextSingle(minValue, maxValue);
            if (result != maxValue)
                return result;
        }
    }

    public double NextDoubleInclusive(double minValue, double maxValue)
    {
        while (true)
        {
            var result = NextDouble(minValue, maxValue);
            if (result != maxValue)
                return result;
        }
    }

    protected RandomGenerationBase()
    {
        this.Seeds = new List<double>();
        this.Seeds = GetSeedsFromTime(1);
    }

    protected RandomGenerationBase(double seed)
    {
        this.Seeds = new List<double>()
        {
            seed
        };
    }

    protected List<double> GetSeedsFromTime(uint numberOfSeeds)
    {
        var result = new List<double>();
        for (var i = 0; i < numberOfSeeds; i++)
        {
            Thread.Sleep(1);
            result.Add(DateTime.Now.Ticks);
        }
        return result;
    }
}