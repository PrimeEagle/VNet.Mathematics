using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using VNet.System;

// ReSharper disable MemberCanBePrivate.Global

namespace VNet.Mathematics.Randomization.Generation;

public abstract class RandomGenerationBase<TSeed, TResult> : IRandomGenerationAlgorithm<TSeed, TResult>
                                                             where TSeed : notnull, INumber<TSeed>
                                                             where TResult : notnull, INumber<TResult>
{
    // ReSharper disable once MemberCanBeProtected.Global
    public new List<TSeed> Seeds { get; init; }
    public new TResult MinValue { get; set; }

    int IRandomGenerationAlgorithm.MinValue { get; set; }
    int IRandomGenerationAlgorithm.MaxValue { get; set; }

    int IRandomGenerationAlgorithm.Next()
    {
        throw new NotImplementedException();
    }

    public int Next(int maxValue)
    {
        throw new NotImplementedException();
    }

    public int Next(int minValue, int maxValue)
    {
        throw new NotImplementedException();
    }
    

    public new TResult MaxValue { get; set; }
    protected const uint NumberOfSeeds = 1;


    protected RandomGenerationBase()
    {
        Seeds = GetSeedsFromTime(NumberOfSeeds);
        MinValue = GetMinValue();
        MaxValue = GetMaxValue();
    }

    protected RandomGenerationBase(TSeed seed)
    {
        Seeds = new List<TSeed> {seed};
        MinValue = GetMaxValue();
        MaxValue = GetMaxValue();
    }

    protected RandomGenerationBase(IEnumerable<TSeed> seeds)
    {
        Seeds = seeds.ToList();
        MinValue = GetMaxValue();
        MaxValue = GetMaxValue();
    }

    protected RandomGenerationBase(string seed)
    {
        Seeds = new List<TSeed> {StringToValue(seed)};
        MinValue = GetMaxValue();
        MaxValue = GetMaxValue();
    }

    protected RandomGenerationBase(IEnumerable<string> seeds)
    {
        Seeds = seeds.Select(StringToValue).ToList();
        MinValue = GetMaxValue();
        MaxValue = GetMaxValue();
    }

    protected RandomGenerationBase(TResult minValue, TResult maxValue)
    {
        Seeds = GetSeedsFromTime(NumberOfSeeds);
        MinValue = minValue;
        MaxValue = maxValue;
    }

    protected RandomGenerationBase(TSeed seed, TResult minValue, TResult maxValue)
    {
        Seeds = new List<TSeed> {seed};
        MinValue = minValue;
        MaxValue = maxValue;
    }

    protected RandomGenerationBase(IEnumerable<TSeed> seeds, TResult minValue, TResult maxValue)
    {
        Seeds = seeds.ToList();
        MinValue = minValue;
        MaxValue = maxValue;
    }

    protected RandomGenerationBase(string seed, TResult minValue, TResult maxValue)
    {
        Seeds = new List<TSeed> {StringToValue(seed)};
        MinValue = minValue;
        MaxValue = maxValue;
    }

    protected RandomGenerationBase(IEnumerable<string> seeds, TResult minValue, TResult maxValue)
    {
        Seeds = seeds.Select(StringToValue).ToList();
        MinValue = minValue;
        MaxValue = maxValue;
    }

    private static TSeed StringToValue(string seed)
    {
        var hash = BitConverter.ToInt32(SHA1.HashData(Encoding.UTF8.GetBytes(seed)));

        return GenericNumber<TSeed>.FromDouble(hash);
    }

    public virtual TResult Next()
    {
        throw new NotImplementedException();
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public virtual TResult Next(TResult maxValue)
    {
        var tempMinValue = GenericNumber<TResult>.FromDouble(0) ?? throw new ArgumentNullException(nameof(maxValue));

        MinValue = GenericNumber<TResult>.FromDouble(0);
        MaxValue = maxValue;

        var result = Next();

        MinValue = tempMinValue;
        MaxValue = maxValue;

        return result;
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public virtual TResult Next(TResult minValue, TResult maxValue)
    {
        MinValue = GenericNumber<TResult>.FromDouble(0);
        MaxValue = maxValue;

        var result = Next();

        MinValue = minValue;
        MaxValue = maxValue;

        return result;
    }

    public virtual float NextSingle()
    {
        return Convert.ToSingle(Next());
    }

    public virtual double NextDouble()
    {
        return Convert.ToDouble(Next());
    }

    public virtual void NextBytes(Span<byte> buffer)
    {
        for (var i = 0; i < buffer.Length; i++) buffer[i] = Convert.ToByte(Next());
    }

    public virtual void NextBytes(byte[] buffer)
    {
        for (var i = 0; i < buffer.Length; i++) buffer[i] = Convert.ToByte(Next());
    }

    public virtual long NextInt64()
    {
        return Convert.ToInt64(Next());
    }

    public virtual long NextInt64(long maxValue)
    {
        return Convert.ToInt64(Next(GenericNumber<TResult>.FromDouble(maxValue)));
    }

    public new long NextInt64(long minValue, long maxValue)
    {
        return Convert.ToInt64(Next(GenericNumber<TResult>.FromDouble(minValue), GenericNumber<TResult>.FromDouble(maxValue)));
    }

    private static TResult GetMaxValue()
    {
        try
        {
            return (TResult) (typeof(TResult).GetField("MaxValue")?.GetValue(null) ?? default(TResult));
        }
        catch
        {
            throw new InvalidOperationException($"Unsupported type {typeof(TResult)}");
        }
    }

    private static TResult GetMinValue()
    {
        try
        {
            return (TResult) (typeof(TResult).GetField("MinValue")?.GetValue(null) ?? default(TResult));
        }
        catch
        {
            throw new InvalidOperationException($"Unsupported type {typeof(TResult)}");
        }
    }

    protected List<TSeed> GetSeedsFromTime(uint numberOfSeeds)
    {
        var result = new List<TSeed>();

        for (var i = 0; i < numberOfSeeds; i++) result.Add(GenericNumber<TSeed>.FromDouble(DateTime.Now.Ticks + 1));

        return result;
    }
}