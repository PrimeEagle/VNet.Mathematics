namespace VNet.Mathematics.Randomization.Generation
{
    public class LcgGenerator : RandomGenerationBase
    {
        private const ulong Multiplier = 6364136223846793005;
        private const ulong Increment = 1442695040888963407;
        private const ulong Modulus = ulong.MaxValue;


        public LcgGenerator() : base() {}
        public LcgGenerator(double seed) : base(seed) { }

        public override int Next()
        {
            var result = (Multiplier * Seeds[0] + Increment) % Modulus;
            
            return (int)result;
        }
    }
}