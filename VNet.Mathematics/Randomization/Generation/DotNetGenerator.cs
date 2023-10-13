namespace VNet.Mathematics.Randomization.Generation
{
    public class DotNetGenerator : RandomGenerationBase
    {
        private readonly Random _random;

        public DotNetGenerator() : base()
        {
            _random = new Random();
        }

        public DotNetGenerator(double seed) : base(seed)
        {
            _random = new Random((int) Seeds[0]);
        }

        public override int Next()
        {
            return _random.Next();
        }
    }
}