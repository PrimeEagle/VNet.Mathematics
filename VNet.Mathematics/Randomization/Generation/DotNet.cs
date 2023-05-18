namespace VNet.Mathematics.Randomization.Generation
{
    public class DotNet : RandomGenerationBase<int, int>
    {
        protected new uint NumberOfSeeds = 1;


        public DotNet() : base() {}
        public DotNet(IEnumerable<int> seeds) : base(seeds) {}
        public DotNet(IEnumerable<string> seeds) : base(seeds) {}
        public DotNet(IEnumerable<int> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) {}
        public DotNet(IEnumerable<string> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) {}
        public DotNet(int minValue, int maxValue) : base(minValue, maxValue) {}

        public override int Next()
        {
            var random = new Random((int) Seeds[0]);

            var result = random.Next(MinValue, MaxValue);

            return result;
        }
    }
}