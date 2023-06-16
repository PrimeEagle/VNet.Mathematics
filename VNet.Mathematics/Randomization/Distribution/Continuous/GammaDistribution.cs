using VNet.Mathematics.Randomization.Generation;
using VNet.System.Conversion;

namespace VNet.Mathematics.Randomization.Distribution.Continuous
{
    public class GammaDistribution : RandomDistributionBase, IGammaDistributionAlgorithm
    {
        private double _shape;
        private double _scale;


        public double Shape
        {
            get => _shape;
            set
            {
                if (value < 0d) throw new ArgumentOutOfRangeException(nameof(Shape), "Must be a positive number.");
                _shape = value;
            }
        }

        public double Scale
        {
            get => _scale;
            set
            {
                if (value < 0d) throw new ArgumentOutOfRangeException(nameof(Scale), "Must be a positive number.");
                _scale = value;
            }
        }

        public GammaDistribution() : base()
        {
        }

        public GammaDistribution(double shape, double scale) : base()
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            Shape = shape;
            Scale = scale;
        }

        public GammaDistribution(IRandomGenerationAlgorithm randomGenerator) : base(randomGenerator)
        {
        }

        public GammaDistribution(IRandomGenerationAlgorithm randomGenerator, double shape, double scale) : base(randomGenerator)
        {
            if (shape < 0d) throw new ArgumentOutOfRangeException(nameof(shape), "Must be a positive number.");
            if (scale < 0d) throw new ArgumentOutOfRangeException(nameof(scale), "Must be a positive number.");

            Shape = shape;
            Scale = scale;
        }

        private double Normal()
        {
            double u1 = _randomGenerator.Next();
            double u2 = _randomGenerator.Next();

            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        }

        protected override T NextValue<T>()
        {
            // Use the Marsaglia-Tsang method for generating Gamma random variables
            switch (Shape)
            {
                case >= 1:
                {
                    var d = Shape - 1.0 / 3.0;
                    var c = 1.0 / Math.Sqrt(9.0 * d);
                    for (; ; )
                    {
                        double x;
                        double v;
                        do
                        {
                            x = Normal();
                            v = 1.0 + c * x;
                        } while (v <= 0.0);

                        v = v * v * v;
                        var u = _randomGenerator.NextDouble();
                        if ((u < 1.0 - 0.0331 * (x * x) * (x * x)) || (Math.Log(u) < 0.5 * x * x + d * (1.0 - v + Math.Log(v))))
                            return GenericNumber<T>.FromDouble(Scale * d * v);
                    }
                }
                case <= 0.0:
                    throw new ArgumentOutOfRangeException(nameof(Shape), "Must be positive.");
                default:
                {
                    var g = 1.0;
                    for (var i = 0; i < Shape; i++)
                        g *= _randomGenerator.NextDouble();
                    g = -Math.Log(g);

                    return GenericNumber<T>.FromDouble(Scale * g);
                }
            }
        }
    }
}