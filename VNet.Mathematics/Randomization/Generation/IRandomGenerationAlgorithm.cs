using System.Numerics;

namespace VNet.Mathematics.Randomization.Generation;

public interface IRandomGenerationAlgorithm : IRandomizationAlgorithm
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int Next();
    public int Next(int maxValue);
    public int Next(int minValue, int maxValue);
    public float NextSingle();
    public double NextDouble();
    public void NextBytes(Span<byte> buffer);
    public void NextBytes(byte[] buffer);
    public long NextInt64();
    public long NextInt64(long maxValue);
    public long NextInt64(long minValue, long maxValue);
}

public interface IRandomGenerationAlgorithm<out TSeed, TResult> : IRandomGenerationAlgorithm 
                                                                  where TSeed : notnull, INumber<TSeed> 
                                                                  where TResult : notnull, INumber<TResult>
{
    public new TResult MinValue { get; set; }
    public new TResult MaxValue { get; set; }
    public new TResult Next();
    public new TResult Next(TResult maxValue);
    public new TResult Next(TResult minValue, TResult maxValue);
    public new float NextSingle();
    public new double NextDouble();
    public new void NextBytes(Span<byte> buffer);
    public new void NextBytes(byte[] buffer);
    public new long NextInt64();
    public new long NextInt64(long maxValue);
    public new long NextInt64(long minValue, long maxValue);
}