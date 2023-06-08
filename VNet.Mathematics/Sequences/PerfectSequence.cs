//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.Sequences
//{
//    public List<int> GeneratePerfectNumberSequence(int length)
//    {
//        List<int> sequence = new List<int>();
//        int number = 2;

//        while (sequence.Count < length)
//        {
//            int sum = 1; // 1 is a divisor of every number

//            for (int i = 2; i * i <= number; i++)
//            {
//                if (number % i == 0)
//                {
//                    if (i * i != number)
//                    {
//                        sum = sum + i + number / i;
//                    }
//                    else
//                    {
//                        sum = sum + i;
//                    }
//                }
//            }

//            if (sum == number)
//            {
//                sequence.Add(number);
//            }

//            number++;
//        }

//        return sequence;
//    }

//}
