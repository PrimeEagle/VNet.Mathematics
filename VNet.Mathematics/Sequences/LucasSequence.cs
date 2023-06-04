using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateLucasNumberSequence(int length)
    {
        List<int> sequence = new List<int>();
        int a = 2;
        int b = 1;

        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                sequence.Add(a);
            }
            else if (i == 1)
            {
                sequence.Add(b);
            }
            else
            {
                int temp = a;
                a = b;
                b = temp + b;
                sequence.Add(b);
            }
        }
        return sequence;
    }

}
