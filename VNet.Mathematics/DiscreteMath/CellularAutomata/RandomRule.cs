//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    public class RandomRule : IRule<bool>
//    {
//        private Random random;

//        public RandomRule()
//        {
//            random = new Random();
//        }

//        public bool GetNextState(bool[] neighborStates)
//        {
//            int randomIndex = random.Next(neighborStates.Length);
//            return neighborStates[randomIndex];
//        }
//    }
//}
