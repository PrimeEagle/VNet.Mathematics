// ReSharper disable MemberCanBePrivate.Global
namespace VNet.Mathematics.Randomization.Generation
{
    public class Dice
    {
        public IRandomGenerationAlgorithm RandomGenerator { get; set; }

        public Dice()
        {
            RandomGenerator = new DotNetGenerator();
        }

        public Dice(IRandomGenerationAlgorithm randomGenerator)
        {
            RandomGenerator = randomGenerator;
        }

        public int Roll(DiceType diceType, int numberOfRolls)
        {
            return Roll((int) diceType, numberOfRolls);
        }

        public int Roll(DiceType diceType)
        {
            return Roll((int)diceType, 1);
        }

        public int Roll(int numberOfSides)
        {
            return Roll(numberOfSides, 1);
        }

        public int Roll(int numberOfSides, int numberOfRolls)
        {
            var sum = 0;

            for (var i = 1; i < numberOfRolls; i++)
            {
                sum += (int)RandomGenerator.NextInclusive(1, numberOfSides);
            }

            return sum;
        }
    }
}