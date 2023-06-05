namespace VNet.Mathematics.Randomization.Generation
{
    public class DotNetGenerator : RandomGenerationBase<int, int>
    {
        protected new uint NumberOfSeeds = 1;


        public DotNetGenerator() : base() {}
        public DotNetGenerator(IEnumerable<int> seeds) : base(seeds) {}
        public DotNetGenerator(IEnumerable<string> seeds) : base(seeds) {}
        public DotNetGenerator(IEnumerable<int> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) {}
        public DotNetGenerator(IEnumerable<string> seeds, int minValue, int maxValue) : base(seeds, minValue, maxValue) {}
        public DotNetGenerator(int minValue, int maxValue) : base(minValue, maxValue) {}

        public override int Next()
        {
            var random = new Random((int) Seeds[0]);

            var result = random.Next(MinValue, MaxValue);

            return result;
        }
    }
}