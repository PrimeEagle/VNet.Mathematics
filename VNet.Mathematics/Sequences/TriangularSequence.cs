using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateTriangularNumberSequence(int length)
    {
        List<int> sequence = new List<int>();
        int sum = 0;
        for (int i = 1; i <= length; i++)
        {
            sum += i;
            sequence.Add(sum);
        }
        return sequence;
    }

}
