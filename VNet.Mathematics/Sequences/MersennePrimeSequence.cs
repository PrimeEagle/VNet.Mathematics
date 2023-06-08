//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.Sequences
//{
//    public List<int> GenerateMersennePrimeSequence(int length)
//    {
//        List<int> sequence = new List<int>();
//        int count = 0;
//        int number = 2;

//        while (count < length)
//        {
//            if (IsPrime(number))
//            {
//                int mersenneNumber = (int)Math.Pow(2, number) - 1;
//                if (IsPrime(mersenneNumber))
//                {
//                    sequence.Add(mersenneNumber);
//                    count++;
//                }
//            }
//            number++;
//        }
//        return sequence;
//    }

//    private bool IsPrime(int number)
//    {
//        if (number <= 1)
//        {
//            return false;
//        }
//        for (int i = 2; i * i <= number; i++)
//        {
//            if (number % i == 0)
//            {
//                return false;
//            }
//        }
//        return true;
//    }

//}
