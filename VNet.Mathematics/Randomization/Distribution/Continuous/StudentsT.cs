using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class StudentsT : RandomDistributionBase, IContinuousRandomDistributionAlgorithm
    {
        private readonly double _mean;
        private readonly double _standardDeviation;
        private readonly int _degreesOfFreedom;
        private readonly IGaussianDistributionAlgorithm _gaussianDistribution;
        private readonly IChiSquareDistributionAlgorithm _chiSquareDistribution;

        public StudentsT(double mean, double standardDeviation, int degreesOfFreedom) : base()
        {
            if (mean <= 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");
            if (degreesOfFreedom <= 0d) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _degreesOfFreedom = degreesOfFreedom;
            _chiSquareDistribution = new ChiSquare(_randomGenerator);
            _gaussianDistribution = new Gaussian(_randomGenerator);
        }

        public StudentsT(IRandomGenerationAlgorithm randomGenerator, double mean, double standardDeviation, int degreesOfFreedom) : base(randomGenerator)
        {
            if (mean <= 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");
            if (degreesOfFreedom <= 0d) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _degreesOfFreedom = degreesOfFreedom;
            _chiSquareDistribution = new ChiSquare(randomGenerator);
            _gaussianDistribution = new Gaussian(randomGenerator);
        }

        public StudentsT(double mean, double standardDeviation, int degreesOfFreedom, IChiSquareDistributionAlgorithm chiSquareDistribution, IGaussianDistributionAlgorithm gaussianDistribution) : base()
        {
            if (mean <= 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");
            if (degreesOfFreedom <= 0d) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _degreesOfFreedom = degreesOfFreedom;
            _chiSquareDistribution = chiSquareDistribution;
            _gaussianDistribution = gaussianDistribution;
        }

        public StudentsT(IRandomGenerationAlgorithm randomGenerator, double mean, double standardDeviation, int degreesOfFreedom, IChiSquareDistributionAlgorithm chiSquareDistribution, IGaussianDistributionAlgorithm gaussianDistribution) : base(randomGenerator)
        {
            if (mean <= 0d) throw new ArgumentOutOfRangeException(nameof(mean), "Must be a positive number.");
            if (standardDeviation <= 0d) throw new ArgumentOutOfRangeException(nameof(standardDeviation), "Must be a positive number.");
            if (degreesOfFreedom <= 0d) throw new ArgumentOutOfRangeException(nameof(degreesOfFreedom), "Must be a positive number.");

            _mean = mean;
            _standardDeviation = standardDeviation;
            _degreesOfFreedom = degreesOfFreedom;
            _chiSquareDistribution = chiSquareDistribution;
            _gaussianDistribution = gaussianDistribution;
        }

        protected override T NextValue<T>()
        {
            _gaussianDistribution.Mean = _mean;
            _gaussianDistribution.StandardDeviation = _standardDeviation;
            var z = _gaussianDistribution.NextDouble();

            _chiSquareDistribution.DegreesOfFreedom = _degreesOfFreedom;
            var y = _chiSquareDistribution.NextDouble();

            // if Z is a standard normal random variable and Y is a chi-square distributed random variable
            // with v degrees of freedom, then X = Z / sqrt(Y/v) has a Student's t-distribution with v degrees of freedom
            return GenericNumber<T>.FromDouble(z / Math.Sqrt(y / _chiSquareDistribution.DegreesOfFreedom));
        }
    }
}