using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateBellNumberSequence(int length)
    {
        int[,] bell = new int[length + 1, length + 1];
        bell[0, 0] = 1;

        for (int i = 1; i <= length; i++)
        {
            // Explicitly fill for j = 0
            bell[i, 0] = bell[i - 1, i - 1];

            // Fill for remaining values of j
            for (int j = 1; j <= i; j++)
            {
                bell[i, j] = bell[i - 1, j - 1] + bell[i, j - 1];
            }
        }

        // Create Bell number sequence
        List<int> sequence = new List<int>();
        for (int i = 0; i < length; i++)
        {
            sequence.Add(bell[i, 0]);
        }

        return sequence;
    }

}
