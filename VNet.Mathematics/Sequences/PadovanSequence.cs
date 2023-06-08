//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.Sequences
//{
//    public List<int> GeneratePadovanSequence(int length)
//    {
//        List<int> sequence = new List<int>();
//        sequence.Add(1);
//        sequence.Add(1);
//        sequence.Add(1);

//        for (int i = 3; i < length; i++)
//        {
//            sequence.Add(sequence[i - 2] + sequence[i - 3]);
//        }

//        return sequence.GetRange(0, length);
//    }

//}
