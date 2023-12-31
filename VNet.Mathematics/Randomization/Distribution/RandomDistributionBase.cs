﻿using System.Numerics;
using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution
{
    public abstract class RandomDistributionBase : IRandomDistributionAlgorithm
    {
        protected readonly IRandomGenerationAlgorithm _randomGenerator;


        protected RandomDistributionBase()
        {
            _randomGenerator = new DotNetGenerator();
        }

        protected RandomDistributionBase(IRandomGenerationAlgorithm randomGenerator)
        {
            _randomGenerator = randomGenerator;
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
                return GenericNumber<T>.FromDouble(_randomGenerator.Next());
            }
            else if (typeof(T) == typeof(long))
            {
                return GenericNumber<T>.FromDouble(_randomGenerator.NextLong());
            }
            else if (typeof(T) == typeof(float))
            {
                return GenericNumber<T>.FromDouble(_randomGenerator.NextSingle());
            }
            else if (typeof(T) == typeof(double))
            {
                return GenericNumber<T>.FromDouble(_randomGenerator.NextDouble());
            }
            else
            {
                return default;
            }
        }
    }
}