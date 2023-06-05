using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Discrete;

public class PoissonDistribution : RandomDistributionBase, IDiscreteRandomDistributionAlgorithm
{
    private readonly double _lambda;

    public PoissonDistribution(double lambda) : base()
    {
        _lambda = lambda;
    }

    public PoissonDistribution(IRandomGenerationAlgorithm randomGenerator, double lambda) : base(randomGenerator)
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
            p *= GenericNumber<T>.ToDouble(u);
        } while (p > l);

        return GenericNumber<T>.FromDouble(k - 1);
    }
}