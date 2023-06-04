using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GeneratePowerOfTwoSequence(int length)
    {
        List<int> sequence = new List<int>();
        for (int i = 0; i < length; i++)
        {
            sequence.Add((int)Math.Pow(2, i));
        }
        return sequence;
    }

}
