using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateArithmeticSequence(int start, int difference, int length)
    {
        List<int> sequence = new List<int>();
        for (int i = 0; i < length; i++)
        {
            sequence.Add(start + i * difference);
        }
        return sequence;
    }

}
