//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    public class Rule54Rule : IRule<bool>
//    {
//        public bool GetNextState(bool[] neighborStates)
//        {
//            bool left = neighborStates[0];
//            bool center = neighborStates[1];
//            bool right = neighborStates[2];

//            if (left && !center && !right)
//                return true;
//            else if (left && !center && right)
//                return true;
//            else if (left && center && !right)
//                return true;
//            else if (left && center && right)
//                return false;
//            else if (!left && !center && !right)
//                return false;
//            else if (!left && !center && right)
//                return true;
//            else if (!left && center && !right)
//                return true;
//            else // (!left && center && right)
//                return true;
//        }
//    }
//}
