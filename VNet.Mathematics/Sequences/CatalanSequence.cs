//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.Sequences
//{
//    public List<int> GenerateCatalanNumberSequence(int length)
//    {
//        List<int> catalan = new List<int>(new int[length]);

//        // Initialize the first two values
//        catalan[0] = 1;
//        if (length > 1)
//        {
//            catalan[1] = 1;

//            // Fill up remaining values
//            for (int i = 2; i < length; i++)
//            {
//                catalan[i] = 0;
//                for (int j = 0; j < i; j++)
//                    catalan[i] += catalan[j] * catalan[i - j - 1];
//            }
//        }
//        return catalan;
//    }

//}
