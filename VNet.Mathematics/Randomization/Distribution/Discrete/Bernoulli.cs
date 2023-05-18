using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete;

public class Bernoulli : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
{
    private readonly double _probabilityOfSuccess;

    public Bernoulli(double probabilityOfSuccess) : base()
    {
        if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
        
        _probabilityOfSuccess = probabilityOfSuccess;
    }

    public Bernoulli(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess) : base(randomGenerator)
    {
        if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
       
        _probabilityOfSuccess = probabilityOfSuccess;
    }

    protected override T NextValue<T>()
    {
        return _randomGenerator.NextDouble() < _probabilityOfSuccess ? Generic.ConvertFromObject<T>(1) : Generic.ConvertFromObject<T>(0);
    }
}