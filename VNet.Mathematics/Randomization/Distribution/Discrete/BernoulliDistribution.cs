﻿using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete;

public class BernoulliDistribution : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
{
    private readonly double _probabilityOfSuccess;

    public BernoulliDistribution(double probabilityOfSuccess) : base()
    {
        if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
        
        _probabilityOfSuccess = probabilityOfSuccess;
    }

    public BernoulliDistribution(IRandomGenerationAlgorithm randomGenerator, double probabilityOfSuccess) : base(randomGenerator)
    {
        if (probabilityOfSuccess is < 0d or > 1.0d) throw new ArgumentOutOfRangeException(nameof(probabilityOfSuccess), "Must be between 0 and 1.");
       
        _probabilityOfSuccess = probabilityOfSuccess;
    }

    protected override T NextValue<T>()
    {
        return _randomGenerator.NextDouble() < _probabilityOfSuccess ? GenericNumber<T>.FromDouble(1) : GenericNumber<T>.FromDouble(0);
    }
}