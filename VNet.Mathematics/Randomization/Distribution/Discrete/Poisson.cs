using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Discrete;

public class Poisson : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
{
    private readonly double _lambda;

    public Poisson(double lambda) : base()
    {
        _lambda = lambda;
    }

    public Poisson(IRandomGenerationAlgorithm randomGenerator, double lambda) : base(randomGenerator)
    {
        _lambda = lambda;
    }

    protected override T NextValue<T>()
    {
        var l = Math.Exp(-_lambda);
        var k = 0d;
        var p = 1.0d;

        do
        {
            k++;
            var u = NextRandomValue<T>();
            p *= Generic.ConvertType<T, double>(u);
        } while (p > l);

        return Generic.ConvertFromObject<T>(k - 1);
    }
}