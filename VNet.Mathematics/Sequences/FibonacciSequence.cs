using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateFibonacciSequence(int length)
    {
        List<int> sequence = new List<int>();
        int a = 0;
        int b = 1;

        for (int i = 0; i < length; i++)
        {
            sequence.Add(a);
            int temp = a;
            a = b;
            b = temp + b;
        }
        return sequence;
    }

}
