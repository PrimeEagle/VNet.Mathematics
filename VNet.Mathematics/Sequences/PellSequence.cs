using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GeneratePellNumberSequence(int length)
    {
        List<int> sequence = new List<int>();

        for (int i = 0; i < length; i++)
        {
            if (i == 0)
            {
                sequence.Add(0);
            }
            else if (i == 1)
            {
                sequence.Add(1);
            }
            else
            {
                sequence.Add(2 * sequence[i - 1] + sequence[i - 2]);
            }
        }

        return sequence;
    }

}
