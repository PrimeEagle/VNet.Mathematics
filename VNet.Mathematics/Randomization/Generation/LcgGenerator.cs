namespace VNet.Mathematics.Randomization.Generation
{
    public class LcgGenerator : RandomGenerationBase<ulong, ulong>
    {
        private const ulong Multiplier = 6364136223846793005;
        private const ulong Increment = 1442695040888963407;
        private const ulong Modulus = ulong.MaxValue;


        public LcgGenerator() : base() {}
        public LcgGenerator(IEnumerable<ulong> seeds) : base(seeds) {}
        public LcgGenerator(IEnumerable<string> seeds) : base(seeds) {}
        public LcgGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue) {}
        public LcgGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue) {}
        public LcgGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue) {}

        public override ulong Next()
        {
            Seeds[0] = (Multiplier * Seeds[0] + Increment) % Modulus;
            
            return Seeds[0] % (MaxValue - MinValue + 1) + MinValue;
        }
    }
}