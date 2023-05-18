using System.Numerics;
using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution
{
    public abstract class RandomDistributionBase : IRandomDistributionAlgorithm
    {
        protected readonly IRandomGenerationAlgorithm _randomGenerator;


        protected RandomDistributionBase()
        {
            _randomGenerator = new DotNet();
            _randomGenerator.MinValue = 0;
            _randomGenerator.MaxValue = 2;
        }

        protected RandomDistributionBase(IRandomGenerationAlgorithm randomGenerator)
        {
            _randomGenerator = randomGenerator;
            _randomGenerator.MinValue = 0;
            _randomGenerator.MaxValue = 2;
        }
 
        public int Next()
        {
            return NextValue<int>();
        }

        public float NextSingle()
        {
            return NextValue<float>();
        }

        public double NextDouble()
        {
            return NextValue<double>();
        }

        public long NextInt64()
        {
            return NextValue<long>();
        }

        protected virtual T NextValue<T>() where T : notnull, INumber<T>
        {
            throw new NotImplementedException();
        }

        protected T NextRandomValue<T>() where T : notnull, INumber<T>
        {
            if (typeof(T) == typeof(int))
            {
                return Generic.ConvertFromObject<T>(_randomGenerator.Next());
            }
            else if (typeof(T) == typeof(long))
            {
                return Generic.ConvertFromObject<T>(_randomGenerator.NextInt64());
            }
            else if (typeof(T) == typeof(float))
            {
                return Generic.ConvertFromObject<T>(_randomGenerator.NextSingle());
            }
            else if (typeof(T) == typeof(double))
            {
                return Generic.ConvertFromObject<T>(_randomGenerator.NextDouble());
            }
            else
            {
                return default;
            }
        }
    }
}