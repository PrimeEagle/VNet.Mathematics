//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    public class MajorityRule : IRule<bool>
//    {
//        public bool GetNextState(bool[] neighborStates)
//        {
//            int trueCount = 0;
//            int falseCount = 0;

//            foreach (bool state in neighborStates)
//            {
//                if (state)
//                    trueCount++;
//                else
//                    falseCount++;
//            }

//            return trueCount > falseCount;
//        }
//    }
//}
